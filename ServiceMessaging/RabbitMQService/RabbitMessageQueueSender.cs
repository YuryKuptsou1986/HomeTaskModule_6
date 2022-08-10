using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using ServiceMessaging.Configuration;
using ServiceMessaging.MessageQueue;
using ServiceMessaging.RabbitMQService.Connection;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ServiceMessaging.RabbitMQService
{
    public class RabbitMessageQueueSender : RabbitMessageQueueBase, IMessageQueueSender, IDisposable
    {
        private readonly IRabbitMqConnection _connection;
        
        private readonly int _retryCount;

        private IModel _consumerChannel;

        public RabbitMessageQueueSender(IRabbitMqConnection connection, IMessageQueueSettingProvider settingProvider)
        {
            _connection = connection;
            _settings = settingProvider.ProvideSettings();
        }

        public void Dispose()
        {
            if (_consumerChannel != null) {
                _consumerChannel.Dispose();
            }
        }

        public void Publish(MessageQueueEvent messageEvent)
        {
            using (var channel = _connection.CreateModel()) {
                var eventName = messageEvent.GetType().Name;
                
                ExchangeDeclare(channel);
                DeclareQueue(channel);
                BindQueue(channel, eventName);

                var body = CreateQueueMessageBody(messageEvent);

                var policy = CreateSenderPolicy(_retryCount);
                policy.Execute(() => {
                    SendQueueMessage(channel, body, _settings.BrokerName, eventName);
                });
            }
        }

        private RetryPolicy CreateSenderPolicy(int retryCount)
        {
            return Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => {
                    Debug.WriteLine($"Publish again");
                });
        }

        private byte[] CreateQueueMessageBody(MessageQueueEvent messageEvent)
        {
            var message = JsonConvert.SerializeObject(messageEvent);
            return Encoding.UTF8.GetBytes(message);
        }

        private void SendQueueMessage(IModel channel, byte[] body, string brokerName, string eventName)
        {
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            channel.BasicPublish(exchange: brokerName,
                             routingKey: eventName,
                             mandatory: true,
                             basicProperties: properties,
                             body: body);
        }
    }
}

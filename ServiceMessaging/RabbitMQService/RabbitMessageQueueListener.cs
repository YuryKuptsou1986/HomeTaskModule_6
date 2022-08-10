using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceMessaging.Configuration;
using ServiceMessaging.MessageQueue;
using ServiceMessaging.MessageQueue.Handler;
using ServiceMessaging.MessageQueue.Subscription;
using ServiceMessaging.RabbitMQService.Connection;
using System.Text;

namespace ServiceMessaging.RabbitMQService
{
    public class RabbitMessageQueueListener : RabbitMessageQueueBase, IMessageQueueListener
    {
        private readonly IRabbitMqConnection _connection;
        private readonly IMessageQueueSubscriptionsManager _subsManager;
        private readonly IServiceScope _serviceScope;
        private IModel _consumerChannel;

        public RabbitMessageQueueListener(IServiceScope serviceScope, IRabbitMqConnection connection, IMessageQueueSubscriptionsManager manager, IMessageQueueSettingProvider settingProvider)
        {
            _connection = connection;
            _subsManager = manager;
            _settings = settingProvider.ProvideSettings();
            _serviceScope = serviceScope;
            _consumerChannel = CreateConsumerChannel();
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        public void Subscribe<TEvent, THandler>()
           where TEvent : MessageQueueEvent
           where THandler : IMessageQueueEventHadler<TEvent>
        {
            var eventName = _subsManager.GetEventKey<TEvent>();

            var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
            _subsManager.AddSubscription<TEvent, THandler>();

            if (!containsKey) {
                using (var channel = _connection.CreateModel()) {
                    DeclareQueue(channel);
                    BindQueue(channel, eventName);
                }
            }
        }

        public void UnSubscribe<TEvent, THandler>()
           where TEvent : MessageQueueEvent
           where THandler : IMessageQueueEventHadler<TEvent>
        {
            _subsManager.RemoveSubscription<TEvent, THandler>();
        }

        public void Dispose()
        {
            if (_consumerChannel != null) {
                _consumerChannel.Dispose();
            }

            _subsManager.Clear();
        }

        private IModel CreateConsumerChannel()
        {
            var channel = _connection.CreateModel();

            ExchangeDeclare(channel);
            DeclareQueue(channel);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) => {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                await ProcessEvent(eventName, message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(_settings.QueueName, false, consumer: consumer);

            channel.CallbackException += (sender, ea) => {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_subsManager.HasSubscriptionsForEvent(eventName)) {
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions) {
                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IMessageQueueEventHadler<>).MakeGenericType(eventType);
                    var subscriptionObject = _serviceScope.ServiceProvider.GetService(subscription);
                    await (Task)concreteType.GetMethod("Handle").Invoke(subscriptionObject, new object[] { integrationEvent });
                }
            }
        }

        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            using (var channel = _connection.CreateModel()) {
                UnbindQueue(channel, eventName);
            }
        }
    }
}

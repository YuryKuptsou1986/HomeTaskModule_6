using RabbitMQ.Client;
using ServiceMessaging.Configuration;

namespace ServiceMessaging.RabbitMQService
{
    public abstract class RabbitMessageQueueBase
    {
        protected MessageQueueSettings _settings;

        protected void DeclareQueue(IModel channel)
        {
            channel.QueueDeclare(queue: _settings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected void ExchangeDeclare(IModel channel)
        {
            channel.ExchangeDeclare(exchange: _settings.BrokerName, type: "direct");
        }

        protected void BindQueue(IModel channel, string eventName)
        {
            channel.QueueBind(queue: _settings.QueueName, exchange: _settings.BrokerName, routingKey: eventName);
        }

        protected void UnbindQueue(IModel channel, string eventName)
        {
            channel.QueueUnbind(queue: _settings.QueueName, exchange: _settings.BrokerName, routingKey: eventName);
        }
    }
}

using RabbitMQ.Client;

namespace ServiceMessaging.RabbitMQService.Connection
{
    public interface IRabbitMqConnectionFactory
    {
        public IConnectionFactory CreateConnectionFactory();
    }
}

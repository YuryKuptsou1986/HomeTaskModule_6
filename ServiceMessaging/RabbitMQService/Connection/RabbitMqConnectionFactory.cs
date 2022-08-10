using RabbitMQ.Client;
using ServiceMessaging.Configuration;

namespace ServiceMessaging.RabbitMQService.Connection
{
    public class RabbitMqConnectionFactory : IRabbitMqConnectionFactory
    {
        private readonly IConnectionFactory _connectionFactory;

        public RabbitMqConnectionFactory(IMessageQueueSettingProvider settingsProvider)
        {
            _connectionFactory = new ConnectionFactory() {
                HostName = settingsProvider.ProvideSettings().ConnectionString
            };
        }

        public IConnectionFactory CreateConnectionFactory()
        {
            return _connectionFactory;
        }
    }
}

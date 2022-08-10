using Microsoft.Extensions.Configuration;

namespace ServiceMessaging.Configuration
{
    public class MessageQueueSettingProvider : IMessageQueueSettingProvider
    {
        private readonly IConfiguration _configuration;
        private const int DefaultRetryCount = 5;
        private const string ConfigurationSection = "EventBusConfiguration";
        private const string EventBusConnectionKey = $"{ConfigurationSection}:EventBusConnection";
        private const string RetryCountKey = $"{ConfigurationSection}:RetryCount";
        private const string BrokerNameKey = $"{ConfigurationSection}:BrokerName";
        private const string QueueNameKey = $"{ConfigurationSection}:QueueName";

        public MessageQueueSettingProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MessageQueueSettings ProvideSettings()
        {
            var settings = new MessageQueueSettings();

            settings.ConnectionString = _configuration.GetSection(EventBusConnectionKey).Value;
            settings.BrokerName = _configuration.GetSection(BrokerNameKey).Value;
            settings.QueueName = _configuration.GetSection(BrokerNameKey).Value;

            settings.RetryCount = int.TryParse(_configuration.GetSection(RetryCountKey).Value, out int retryCount)
                ? retryCount
                : DefaultRetryCount;

            return settings;
        }
    }
}

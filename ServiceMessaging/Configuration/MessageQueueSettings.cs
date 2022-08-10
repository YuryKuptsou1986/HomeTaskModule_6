namespace ServiceMessaging.Configuration
{
    public class MessageQueueSettings
    {
        public string QueueName { get; set; }
        public string BrokerName { get; set; }
        public string ConnectionString { get; set; }
        public int RetryCount { get; set; }
    }
}

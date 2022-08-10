namespace ServiceMessaging.Configuration
{
    public interface IMessageQueueSettingProvider
    {
        public MessageQueueSettings ProvideSettings();
    }
}

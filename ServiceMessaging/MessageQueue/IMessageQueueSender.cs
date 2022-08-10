namespace ServiceMessaging.MessageQueue
{
    public interface IMessageQueueSender
    {
        public void Publish(MessageQueueEvent messageEvent);
    }
}

namespace ServiceMessaging.MessageQueue.Handler
{
    public interface IMessageQueueEventHadler<in TEvent> : IMessageQueueEventHadler
        where TEvent : MessageQueueEvent
    {
        Task Handle(TEvent queueEvent);
    }

    public interface IMessageQueueEventHadler
    {
    }
}

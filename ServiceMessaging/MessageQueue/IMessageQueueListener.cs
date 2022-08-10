using ServiceMessaging.MessageQueue.Handler;

namespace ServiceMessaging.MessageQueue
{
    public interface IMessageQueueListener : IDisposable
    {
        void Subscribe<TEvent, THandler>()
            where TEvent : MessageQueueEvent
            where THandler : IMessageQueueEventHadler<TEvent>;

        void UnSubscribe<TEvent, THandler>()
            where TEvent : MessageQueueEvent
            where THandler : IMessageQueueEventHadler<TEvent>;
    }
}

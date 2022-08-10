using ServiceMessaging.MessageQueue.Handler;

namespace ServiceMessaging.MessageQueue.Subscription
{
    public interface IMessageQueueSubscriptionsManager
    {
        event EventHandler<string> OnEventRemoved;

        bool IsEmpty { get; }

        void Clear();
        string GetEventKey<T>();
        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        IEnumerable<Type> GetHandlersForEvent(string eventName);

        void AddSubscription<TEvent, THandler>()
           where TEvent : MessageQueueEvent
           where THandler : IMessageQueueEventHadler<TEvent>;

        void RemoveSubscription<TEvent, THandler>()
             where TEvent : MessageQueueEvent
             where THandler : IMessageQueueEventHadler<TEvent>;
    }
}

using ServiceMessaging.MessageQueue.Handler;

namespace ServiceMessaging.MessageQueue.Subscription
{
    public class MessageQueueSubscriptionsManager : IMessageQueueSubscriptionsManager
    {
        private readonly Dictionary<string, List<Type>> _handlers = new Dictionary<string, List<Type>>();
        private readonly List<Type> _eventTypes = new List<Type>();
        public event EventHandler<string> OnEventRemoved;

        public void AddSubscription<TEvent, THandler>()
            where TEvent : MessageQueueEvent
            where THandler : IMessageQueueEventHadler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            var handlerType = typeof(THandler);
            if (!HasSubscriptionsForEvent(eventName)) {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s == handlerType)) {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);
            _eventTypes.Add(typeof(TEvent));
        }

        public void RemoveSubscription<TEvent, THandler>()
            where TEvent : MessageQueueEvent
            where THandler : IMessageQueueEventHadler<TEvent>
        {
            var eventName = GetEventKey<TEvent>();
            var handlerType = typeof(THandler);

            if (!HasSubscriptionsForEvent(eventName)) {
                return;
            }

            var handlerToRemove = _handlers[eventName].SingleOrDefault(s => s == handlerType);
            if (handlerToRemove != null) {
                _handlers[eventName].Remove(handlerToRemove);
                if (!_handlers[eventName].Any()) {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null) {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);
        public void Clear() => _handlers.Clear();
        public bool IsEmpty => !_handlers.Keys.Any();
        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);
        public IEnumerable<Type> GetHandlersForEvent(string eventName) => _handlers[eventName];

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            if (OnEventRemoved != null) {
                OnEventRemoved(this, eventName);
            }
        }
    }
}

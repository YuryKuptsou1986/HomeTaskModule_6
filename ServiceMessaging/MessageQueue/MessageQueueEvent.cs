namespace ServiceMessaging.MessageQueue
{
    public class MessageQueueEvent
    {
        public Guid Id { get; private set; }
        public DateTime createdData { get; private set; }

        public MessageQueueEvent()
        {
            Id = Guid.NewGuid();
            createdData = DateTime.UtcNow;
        }
    }
}

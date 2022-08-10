using ServiceMessaging.MessageQueue;

namespace BasketService.Services
{
    public class MessageQueueHostedService : BackgroundService
    {
        private IMessageQueueListener _messageQueueListener;

        public MessageQueueHostedService(IMessageQueueListener messageQueueListener)
        {
            _messageQueueListener = messageQueueListener;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _messageQueueListener.Dispose();
        }
    }
}
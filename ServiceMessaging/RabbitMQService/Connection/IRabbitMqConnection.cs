using RabbitMQ.Client;

namespace ServiceMessaging.RabbitMQService.Connection
{
    public interface IRabbitMqConnection : IDisposable
    {
        bool TryConnect();
        IModel CreateModel();

        bool IsConnected { get; }
    }
}

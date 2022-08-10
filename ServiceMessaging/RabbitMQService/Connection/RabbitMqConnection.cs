using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using ServiceMessaging.Configuration;
using System.Diagnostics;
using System.Net.Sockets;

namespace ServiceMessaging.RabbitMQService.Connection
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly MessageQueueSettings _messageQueueSettings;
        private IConnection _connection;
        private bool _disposed;
        private object _lock = new object();

        public RabbitMqConnection(IRabbitMqConnectionFactory connectionFactory, IMessageQueueSettingProvider settingProvider)
        {
            _connectionFactory = connectionFactory.CreateConnectionFactory();
            _messageQueueSettings = settingProvider.ProvideSettings();
        }

        public IModel CreateModel()
        {
            if (!IsConnected) {
                if (!TryConnect()) {
                    throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
                }
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) {
                return;
            }

            _disposed = true;

            _connection.Dispose();
        }

        public bool TryConnect()
        {
            lock (_lock) {
                var policy = RetryPolicy
                    .Handle<BrokerUnreachableException>()
                    .Or<SocketException>()
                    .WaitAndRetry(_messageQueueSettings.RetryCount
                                    , retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => {
                                        Debug.WriteLine($"Reconect");
                                    });

                policy.Execute(() => _connection = _connectionFactory.CreateConnection());
                if (IsConnected) {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    return true;
                } else {
                    return false;
                }
            }
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;
     
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) {
                return;
            }

            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) {
                return;
            }

            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) {
                return;
            }

            TryConnect();
        }
    }
}

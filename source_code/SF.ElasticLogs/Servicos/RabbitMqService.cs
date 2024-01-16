using System;
using System.Net.Security;
using RabbitMQ.Client;
using SF.ElasticLogs.Modelos;

namespace SF.ElasticLogs.Servicos
{
    public class RabbitMqService : IDisposable
    {
        private readonly RabbitMqConfig _rabbitMqConfig;
        private IConnection _connection;
        private readonly object _connectionLock = new object();

        public RabbitMqService(RabbitMqConfig rabbitMqConfig)
        {
            _rabbitMqConfig = rabbitMqConfig ?? throw new ArgumentNullException(nameof(rabbitMqConfig));
        }

        public IConnection GetConnection(string userName, string password)
        {
            if (_connection == null || !_connection.IsOpen)
            {
                lock (_connectionLock)
                {
                    if (_connection == null || !_connection.IsOpen)
                    {
                        var factory = CreateConnectionFactory(userName, password);
                        _connection = factory.CreateConnection();
                    }
                }
            }

            return _connection;
        }

        private ConnectionFactory CreateConnectionFactory(string userName, string password)
        {
            return new ConnectionFactory
            {
                HostName = _rabbitMqConfig.HostName,
                UserName = userName,
                Password = password,
                VirtualHost = _rabbitMqConfig.VirtualHost,
                Port = _rabbitMqConfig.Port,
                Ssl = new SslOption
                {
                    Enabled = _rabbitMqConfig.Ssl.Enabled,
                    ServerName = _rabbitMqConfig.Ssl.ServerName,
                    AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch |
                                             SslPolicyErrors.RemoteCertificateChainErrors
                }
            };
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
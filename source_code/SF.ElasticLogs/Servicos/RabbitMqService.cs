using System;
using System.Net.Security;
using RabbitMQ.Client;
using SF.ElasticLogs.Modelos;

namespace SF.ElasticLogs.Servicos
{
    public class RabbitMqService
    {
        private readonly RabbitMqConfig _rabbitMqConfig;

        public RabbitMqService(RabbitMqConfig rabbitMqConfig)
        {
            _rabbitMqConfig = rabbitMqConfig ?? throw new ArgumentNullException(nameof(rabbitMqConfig));
        }

        public ConnectionFactory ConfigurarConexao(string userName, string password)
        {
            var resultado = new ConnectionFactory
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

            return resultado;
        }
    }
}

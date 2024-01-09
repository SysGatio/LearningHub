namespace RabbitMQ.Producer.Handlers;

[UsedImplicitly]
internal sealed class LogProducerHandler(ILogger<LogProducerHandler> logger, RabbitMqConfig rabbitMqConfig) : IRequestHandler<QueueLogMessageRequest>
{
    public Task Handle(QueueLogMessageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            using var connection = ConnectionFactory().CreateConnection();
            using var channel = connection.CreateModel();

            foreach (var log in GenerateLogs(request.LogsQuantity))
            {
                var logJson = JsonSerializer.Serialize(log);
                var body = Encoding.UTF8.GetBytes(logJson);

                channel.BasicPublish(exchange: "",
                    routingKey: LearningHub.Domain.MessageQueueLabs.Utils.Constants.OperationQueueName,
                    basicProperties: null, body: body);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error queuing log message.");
            throw;
        }

        return Task.CompletedTask;
    }

    private ConnectionFactory ConnectionFactory()
    {
        var result = new ConnectionFactory
        {
            HostName = rabbitMqConfig.HostName,
            UserName = rabbitMqConfig.UserName,
            Password = rabbitMqConfig.Password,
            VirtualHost = rabbitMqConfig.VirtualHost,
            Port = rabbitMqConfig.Port,
            Ssl = new SslOption
            {
                Enabled = rabbitMqConfig.Ssl.Enabled,
                ServerName = rabbitMqConfig.Ssl.ServerName,
                AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch |
                                         SslPolicyErrors.RemoteCertificateChainErrors
            }
        };

        return result;
    }

    private static IEnumerable<OperationLogDto> GenerateLogs(int requestQuantityOfLogs)
    {
        var random = new Random();

        for (var i = 0; i < requestQuantityOfLogs; i++)
        {
            var logId = Guid.NewGuid();
            var ipAddress = GenerateRandomIpAddress(random);

            var recordType = random.Next(2) == 0 ? RecordType.Success : RecordType.Failure;

            yield return new OperationLogDto
            {
                MessageText = $"{DateTime.Now} Message {logId}",
                InterfaceName = $"Interface {logId}",
                UserName = $"User {logId}",
                IpAddress = ipAddress,
                OccurrenceDate = DateTime.Now,
                RecordType = recordType,
                ExceptionText = recordType == RecordType.Success ? null : $"Exception message {logId}",
                ExceptionStackTrace = recordType == RecordType.Success ? null : $"Stack trace {logId}",
            };
        }
    }

    private static string GenerateRandomIpAddress(Random random)
    {
        var result = $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";

        return result;
    }
}
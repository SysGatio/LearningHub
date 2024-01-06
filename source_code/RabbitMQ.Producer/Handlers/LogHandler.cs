namespace RabbitMQ.Producer.Handlers;

[UsedImplicitly]
internal sealed class LogHandler(ILogger<LogHandler> logger, RabbitMqConfig rabbitMqConfig) : IRequestHandler<QueueLogMessageRequest>
{
    public Task Handle(QueueLogMessageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            using var connection = ConnectionFactory().CreateConnection();
            using var channel = connection.CreateModel();

            foreach (var log in GenerateLogs(request.QuantityOfLogs))
            {
                var logJson = JsonSerializer.Serialize(log);
                var body = Encoding.UTF8.GetBytes(logJson);

                var queueName = log is SuccessLogDto
                    ? LearningHub.Domain.Utils.Constants.RabbitMq.SuccessQueueName
                    : LearningHub.Domain.Utils.Constants.RabbitMq.FailureQueueName;

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
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

    private static List<BaseLogDto> GenerateLogs(int requestQuantityOfLogs)
    {
        var random = new Random();
        var logs = new List<BaseLogDto>();

        for (var i = 0; i < requestQuantityOfLogs; i++)
        {
            var logId = Guid.NewGuid();
            var ipAddress = GenerateRandomIpAddress(random);

            if (random.Next(2) == 0)
            {
                logs.Add(new SuccessLogDto
                {
                    MessageText = $"Success message {logId}",
                    InterfaceName = $"Interface {logId}",
                    UserName = $"User {logId}",
                    IpAddress = ipAddress,
                    OccurrenceDate = DateTime.Now
                });
            }
            else
            {
                logs.Add(new FailureLogDto
                {
                    MessageText = $"Failure message {logId}",
                    InterfaceName = $"Interface {logId}",
                    UserName = $"User {logId}",
                    IpAddress = ipAddress,
                    OccurrenceDate = DateTime.Now,
                    ExceptionText = $"Exception message {logId}",
                    ExceptionStackTrace = $"Stack trace {logId}"
                });
            }
        }

        return logs;
    }

    private static string GenerateRandomIpAddress(Random random)
    {
        return $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
    }
}

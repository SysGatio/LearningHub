namespace RabbitMQ.Consumer.Services;

public class LogConsumerService(ILogger<LogConsumerService> logger, IServiceProvider serviceProvider, RabbitMqConfig rabbitMqConfig) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var connection = CreateConnection();
        using var channel = connection.CreateModel();

        ConsumeQueue(channel, LearningHub.Domain.MessageQueueLabs.Utils.Constants.OperationQueueName);

        while (!stoppingToken.IsCancellationRequested)
        {
            Task.Delay(1000, stoppingToken).Wait(stoppingToken);
        }

        return Task.CompletedTask;
    }

    private void ConsumeQueue(IModel channel, string queueName)
    {
        try
        {
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<Context>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var operationLogDto = JsonConvert.DeserializeObject<OperationLogDto>(message);

                if (operationLogDto == null)
                {
                    return;
                }

                var operationLog = mapper.Map<OperationLog>(operationLogDto);

                context.OperationLog.Add(operationLog);
                context.SaveChanges();
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing message from {queueName}", queueName);
            throw;
        }
      
    }

    private IConnection CreateConnection()
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

        return result.CreateConnection();
    }
}
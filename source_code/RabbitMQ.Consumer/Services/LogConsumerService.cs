namespace RabbitMQ.Consumer.Services;

public class LogConsumerService(ILogger<LogConsumerService> logger, IServiceProvider serviceProvider, RabbitMqConfig rabbitMqConfig) : BackgroundService
{
    private static int _callCount;

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
        var arguments = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "" },
            { "x-dead-letter-routing-key", $"{LearningHub.Domain.MessageQueueLabs.Utils.Constants.OperationRetryQueueName}" }
        };

        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: arguments);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                PersistLogs(message);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing message from {queueName}", queueName);
                channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
            }
        };

        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }

    private void PersistLogs(string message)
    {
        _callCount++;

        if (_callCount > 99)
        {
            throw new Exception("Simulated database failure");
        }

        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<Context>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        var operationLogDto = JsonConvert.DeserializeObject<OperationLogDto>(message);

        if (operationLogDto == null)
        {
            return;
        }

        var operationLog = mapper.Map<OperationLog>(operationLogDto);

        context.OperationLog.Add(operationLog);

        context.SaveChanges();
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
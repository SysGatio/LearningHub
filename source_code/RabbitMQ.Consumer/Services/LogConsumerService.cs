namespace RabbitMQ.Consumer.Services;

public class LogConsumerService(ILogger<LogConsumerService> logger, RabbitMqConfig rabbitMqConfig) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var connection = CreateConnection();
        using var channel = connection.CreateModel();

        ConsumeQueue(channel, LearningHub.Domain.Utils.Constants.RabbitMq.FailureQueueName);
        ConsumeQueue(channel, LearningHub.Domain.Utils.Constants.RabbitMq.SuccessQueueName);

        while (!stoppingToken.IsCancellationRequested)
        {
            Task.Delay(1000, stoppingToken).Wait(stoppingToken);
        }

        return Task.CompletedTask;
    }

    private void ConsumeQueue(IModel channel, string queueName)
    {
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            logger.LogInformation("Received message from {queueName}: {message}", queueName, message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
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
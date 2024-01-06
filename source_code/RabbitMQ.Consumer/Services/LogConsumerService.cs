namespace RabbitMQ.Consumer.Services;

public class LogConsumerService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Lógica para conectar ao RabbitMQ e consumir mensagens
        }

        return Task.CompletedTask;
    }
}

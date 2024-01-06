namespace RabbitMQ.Producer.Configs;

public record RabbitMqConfig
{
    public required string HostName { get; init; }

    public required string UserName { get; init; }

    public required string Password { get; init; }

    public required string VirtualHost { get; init; }

    public required int Port { get; init; }

    public required SslConfig Ssl { get; init; }
}

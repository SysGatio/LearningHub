namespace LearningHub.Domain.MessageQueueLabs.Configs;

[UsedImplicitly]
public record SslConfig
{
    public bool Enabled { get; set; }

    public required string ServerName { get; set; }
}
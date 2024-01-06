namespace LearningHub.Domain.MessageQueueLabs.Dtos.Base;

public record BaseLogDto : IRequest
{
    public string MessageText { get; init; } = string.Empty;

    public string InterfaceName { get; init; } = string.Empty;

    public string UserName { get; init; } = string.Empty;

    public string IpAddress { get; init; } = string.Empty;

    public DateTime OccurrenceDate { get; init; }
}

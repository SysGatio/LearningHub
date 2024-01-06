namespace LearningHub.Domain.MessageQueueLabs.Dtos;

public record OperationLogDto : IRequest
{
    public long? Id { get; init; }

    public required string MessageText { get; init; } = string.Empty;

    public required string InterfaceName { get; init; } = string.Empty;

    public required string UserName { get; init; } = string.Empty;

    public required string IpAddress { get; init; } = string.Empty;

    public required DateTime OccurrenceDate { get; init; }

    public required RecordType RecordType { get; init; }

    public string? ExceptionText { get; init; }

    public string? ExceptionStackTrace { get; init; }
}

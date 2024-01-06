namespace LearningHub.Domain.MessageQueueLabs.Dtos;

public record FailureLogDto : BaseLogDto
{
    public static RecordType RecordType => RecordType.Failure;

    public string ExceptionText { get; init; } = string.Empty;

    public string ExceptionStackTrace { get; init; } = string.Empty;
}

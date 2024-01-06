namespace LearningHub.Domain.MessageQueueLabs.Dtos;

public record SuccessLogDto : BaseLogDto
{
    public static RecordType RecordType => RecordType.Success;
}

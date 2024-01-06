namespace LearningHub.Shared.MessageQueueLabs.Dtos;

public record SuccessLogDto : BaseLog
{
    public static RecordType RecordType => RecordType.Success;
}

namespace LearningHub.Domain.MessageQueueLabs.Entities;

[Table("success-log")]
public class SuccessLog : BaseLog
{
    [Column("record-type")]
    public static RecordType RecordType => RecordType.Success;
}
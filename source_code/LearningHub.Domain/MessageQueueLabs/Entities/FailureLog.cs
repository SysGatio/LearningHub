namespace LearningHub.Domain.MessageQueueLabs.Entities;

[Table("failure-log")]
public class FailureLog : BaseLog
{
    [Column("record-type")]
    public static RecordType RecordType => RecordType.Failure;

    [Column("exception-text")]
    public string ExceptionText { get; set; } = string.Empty;

    [Column("exception-stack-trace")]
    public string ExceptionStackTrace { get; set; } = string.Empty;
}
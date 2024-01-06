namespace LearningHub.Domain.MessageQueueLabs.Entities;

public class OperationLog
{
    [Key]
    [Column("log_id")]
    public long Id { get; set; }

    [Column("message-text")]
    public required string MessageText { get; set; } = string.Empty;

    [Column("interface-name")]
    public required string InterfaceName { get; set; } = string.Empty;

    [Column("user-name")]
    public required string UserName { get; set; } = string.Empty;

    [Column("ip-address")]
    public required string IpAddress { get; set; } = string.Empty;

    [Column("occurrence-date")]
    public required DateTime OccurrenceDate { get; set; }

    [Column("record-type")]
    public required RecordType RecordType { get; set; }

    [Column("exception-text")]
    public string? ExceptionText { get; set; }

    [Column("exception-stack-trace")]
    public string? ExceptionStackTrace { get; set; }
}
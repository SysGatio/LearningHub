namespace LearningHub.Domain.MessageQueueLabs.Entities.Base;

public class BaseLog
{
    [Key]
    [Column("log_id")]
    public long Id { get; set; }
    
    [Column("message-text")]
    public string MessageText { get; set; } = string.Empty;

    [Column("interface-name")]
    public string InterfaceName { get; set; } = string.Empty;

    [Column("user-name")]
    public string UserName { get; set; } = string.Empty;

    [Column("ip-address")]
    public string IpAddress { get; set; } = string.Empty;

    [Column("occurrence-date")]
    public DateTime OccurrenceDate { get; set; }
}
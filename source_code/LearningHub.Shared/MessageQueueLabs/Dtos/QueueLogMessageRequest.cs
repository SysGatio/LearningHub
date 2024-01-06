namespace LearningHub.Shared.MessageQueueLabs.Dtos;

public class QueueLogMessageRequest : IRequest
{
    public int QuantityOfLogs { get; init; }
}

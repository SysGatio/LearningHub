﻿namespace LearningHub.Domain.MessageQueueLabs.Requests;

public class QueueLogMessageRequest : IRequest
{
    public int LogsQuantity { get; init; }
}

namespace LearningHub.Shared.Utils;

public static class Constants
{
    public static class RabbitMq
    {
        public const string SuccessQueueName = "logs_success";
        public const string FailureQueueName = "logs_failure";
    }
}
namespace SF.ElasticLogs.Shared.Modelos.Logs.Base
{
    public abstract class LogBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();


        public DateTime DataHoraOcorrencia { get; private set; } = DateTime.Now;
    }
}

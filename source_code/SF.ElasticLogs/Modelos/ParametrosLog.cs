using System;

namespace SF.ElasticLogs.Modelos
{
    public class ParametrosLog
    {
        public Guid SistemaId { get; set; }

        public string UsuarioRabbitMq { get; set; }

        public string SenhaRabbitMq { get; set; }

        public ParametrosLog(Guid sistemaId, string usuarioRabbitMq, string senhaRabbitMq)
        {
            SistemaId = sistemaId;
            UsuarioRabbitMq = usuarioRabbitMq;
            SenhaRabbitMq = senhaRabbitMq;
        }
    }
}

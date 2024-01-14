using System;
using System.Collections.Generic;
using System.Text;

namespace SF.ElasticLogs.Modelos.Base
{
    public abstract class LogBase
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime DataHoraOcorrencia { get; private set; } = DateTime.Now;
    }
}

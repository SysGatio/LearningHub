using System.ComponentModel.DataAnnotations;
using SF.ElasticLogs.Modelos.Base;

namespace SF.ElasticLogs.Modelos
{
    public class LogAuditoria : LogBase
    {
        [Required]
        public string NomeUsuario { get; set; }

        [Required]
        public string CodigoIdentificadorUsuario { get; set; }

        public string? NumeroEnderecoIp { get; set; }

        [Required]
        public string NomeFuncionalidade { get; set; }

        public string? NomeOperacao { get; set; }

        [Required]
        public string MensagemAuditoria { get; set; }

        public string? CodigoIdentificadorCertificado { get; set; }

        public LogAuditoria(string nomeUsuario, string codigoIdentificadorUsuario, string nomeFuncionalidade, string mensagemAuditoria)
        {
            NomeUsuario = nomeUsuario;
            CodigoIdentificadorUsuario = codigoIdentificadorUsuario;
            NomeFuncionalidade = nomeFuncionalidade;
            MensagemAuditoria = mensagemAuditoria;
        }
    }
}

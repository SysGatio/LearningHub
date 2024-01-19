using System.ComponentModel.DataAnnotations;
using SF.ElasticLogs.Shared.Modelos.Logs.Base;

namespace SF.ElasticLogs.Shared.Modelos.Logs
{
    public class LogAuditoria(
        string nomeUsuario,
        string codigoIdentificadorUsuario,
        string nomeFuncionalidade,
        string mensagemAuditoria,
        string numeroEnderecoIp,
        string nomeOperacao,
        string codigoIdentificadorCertificado) : LogBase
    {
        [Required]
        public string NomeUsuario { get; set; } = nomeUsuario;

        [Required]
        public string CodigoIdentificadorUsuario { get; set; } = codigoIdentificadorUsuario;

        [Required]
        public string NumeroEnderecoIp { get; set; } = numeroEnderecoIp;

        [Required]
        public string NomeFuncionalidade { get; set; } = nomeFuncionalidade;

        [Required]
        public string NomeOperacao { get; set; } = nomeOperacao;

        [Required]
        public string MensagemAuditoria { get; set; } = mensagemAuditoria;

        [Required]
        public string CodigoIdentificadorCertificado { get; set; } = codigoIdentificadorCertificado;
    }
}

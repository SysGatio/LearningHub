using System.ComponentModel.DataAnnotations;
using SF.ElasticLogs.Modelos.Base;
using SF.ElasticLogs.Modelos.Enums;

namespace SF.ElasticLogs.Modelos
{
    public class LogOperacao : LogBase
    {
        [Required]
        public string NomeSistema { get; set; }

        [Required]
        public string NomeFuncionalidade { get; set; }

        public string? NomeUsuario { get; set; }

        public string? CodigoIdentificadorUsuario { get; set; }

        public string? NumeroEnderecoIp { get; set; }

        [Required]
        public TipoRegistro TipoRegistro { get; set; }

        [Required]
        public SubTipoRegistro SubTipoRegistro { get; set; }

        [Required]
        public string MensagemOperacao { get; set; }

        public string? TextoExcecao { get; set; }

        public string? DetalheMensagem { get; set; }

        public string? CodigoIdentificadorCertificado { get; set; }

        public LogOperacao(string nomeSistema, string nomeFuncionalidade, string mensagemOperacao)
        {
            NomeSistema = nomeSistema;
            NomeFuncionalidade = nomeFuncionalidade;
            MensagemOperacao = mensagemOperacao;
        }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SF.ElasticLogs.Shared.Modelos.Logs
{
    public enum SubTipoRegistro
    {
        [Display(Name = "Geral")] 
        [Description("Geral")]
        Geral,
        
        [Display(Name = "Integração")] 
        [Description("Integração")]
        Integracao,

        [Display(Name = "Processo Batch - Aplicação")]
        [Description("Processo Batch - Aplicação")]
        ProcessoBatchAplicacao,
        
        [Display(Name = "Processo Batch - Banco de Dados")]
        [Description("Processo Batch - Banco de Dados")]
        ProcessoBatchBancoDeDados,
    }
}

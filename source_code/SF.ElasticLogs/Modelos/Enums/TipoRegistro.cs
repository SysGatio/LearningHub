using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SF.ElasticLogs.Modelos.Enums
{
    public enum TipoRegistro
    {
        [Display(Name = "Falha")]
        [Description("Falha")]
        Falha,

        [Display(Name = "Sucesso")] 
        [Description("Sucesso")]
        Sucesso
    }
}

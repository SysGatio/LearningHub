using SF.ElasticLogs;
using SF.ElasticLogs.Modelos;

namespace SF.App01.Core.UseCases.LogProducer;

[ApiController, Route("api/[controller]")]
public class LogProducerController(ILogger<LogProducerController> logger, IConfiguration configuration) : ControllerBase
{
    [HttpPost("QueueLogMessage")]
    public IActionResult QueueLogMessage()
    {
        try
        {
            var userName = configuration["Usuario"];
            var password = configuration["Senha"];
            var sistemaId = Guid.Parse(configuration["SistemaId"] ?? string.Empty);

            var parametroLogs = new ParametrosLog(sistemaId, userName ?? string.Empty, password ?? string.Empty);

            var logAuditoria = new LogAuditoria(nomeUsuario: "Monteiro", codigoIdentificadorUsuario: "x435599",
                               nomeFuncionalidade: "LogProducerController", mensagemAuditoria: "Hello World!");

            ElasticLog.GerarLogAuditoria(logAuditoria, parametroLogs);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing your request.{ex.Message}");
        }
    }
}
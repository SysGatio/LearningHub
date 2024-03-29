﻿namespace RabbitMQ.Producer.Controllers;

[ApiController, Route("api/[controller]")]
public class LogProducerController(ILogger<LogProducerController> logger, ISender mediator) : ControllerBase
{
    [HttpPost("QueueLogMessage")]
    public async Task<IActionResult> QueueLogMessage([FromQuery] int logsQuantity)
    {
        try
        {
            await mediator.Send(new QueueLogMessageRequest {LogsQuantity = logsQuantity });

            return Ok("Log message queued successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error queuing log message.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}
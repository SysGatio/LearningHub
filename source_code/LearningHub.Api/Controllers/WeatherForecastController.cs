using System.Net.Security;
using System.Text;
using System.Text.Json;
using LearningHub.Shared.MessageQueueLabs.Dtos;
using LearningHub.Shared.MessageQueueLabs.Dtos.Base;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace LearningHub.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public int value { get; set; } = 100;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "b-b4596503-11e1-4b96-a33b-fd3b84759d5f.mq.sa-east-1.amazonaws.com", // Sem 'amqps://' e porta
            UserName = "Monteirolnx",
            Password = "LauBielElis1@", // Substitua com a senha real
            VirtualHost = "/",
            Port = 5671, // Porta específica para AMQPS
            Ssl = new SslOption
            {
                Enabled = true,
                ServerName = "b-b4596503-11e1-4b96-a33b-fd3b84759d5f.mq.sa-east-1.amazonaws.com",
                AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch |
                                         SslPolicyErrors.RemoteCertificateChainErrors
            }
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            var successQueueName = "logs_success";
            var failureQueueName = "logs_failure";

            var logs = GenerateLogs(); 

            foreach (var log in logs)
            {
                var logJson = JsonSerializer.Serialize(log);
                var body = Encoding.UTF8.GetBytes(logJson);

                var queueName = log is SuccessLogDto ? successQueueName : failureQueueName;
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }


        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private List<BaseLog> GenerateLogs()
    {
        var random = new Random();
        var logs = new List<BaseLog>();

        for (var i = 0; i < value; i++)
        {
            var logId = Guid.NewGuid();
            var ipAddress = GenerateRandomIpAddress(random);

            if (random.Next(2) == 0)
            {
                logs.Add(new SuccessLogDto
                {
                    MessageText = $"Success message {logId}",
                    InterfaceName = $"Interface {logId}",
                    UserName = $"User {logId}",
                    IpAddress = ipAddress,
                    OccurrenceDate = DateTime.Now
                });
            }
            else
            {
                logs.Add(new FailureLogDto
                {
                    MessageText = $"Failure message {logId}",
                    InterfaceName = $"Interface {logId}",
                    UserName = $"User {logId}",
                    IpAddress = ipAddress,
                    OccurrenceDate = DateTime.Now,
                    ExceptionText = $"Exception message {logId}",
                    ExceptionStackTrace = $"Stack trace {logId}"
                });
            }
        }

        return logs;
    }

    string GenerateRandomIpAddress(Random random)
    {
        return $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
    }
}

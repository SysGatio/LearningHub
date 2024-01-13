namespace SF.App01.UseCases.WeatherForecast.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    ];

    private readonly ILogger<WeatherForecastController> _logger = logger;

    [HttpGet("InvalidLocation", Name = "SimulateInvalidLocation")]
    public IResult InvalidLocation()
    {
        var result = Result.Failure(WeatherForecastErrors.InvalidLocation);

        return result.Match(onSuccess: Results.NoContent, onFailure: Results.BadRequest);
    }

    [HttpGet("Success", Name = "SimulateSucess")]
    public IResult Success()
    {
        try
        {
            throw new Exception("Simulated exception");
            var weatherForecasts = Enumerable.Range(1, 5).Select(index => new Domain.WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            var result = Result<Domain.WeatherForecast[]>.Success(weatherForecasts);

            return result.Match(
                onSuccess: () => Results.Ok(result.Value),
                onFailure: error => Results.BadRequest(new { Error = error }));
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message);
        }
    }
}


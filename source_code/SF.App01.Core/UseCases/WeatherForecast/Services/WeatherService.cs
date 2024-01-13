namespace SF.App01.Core.UseCases.WeatherForecast.Services;

public class WeatherService
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public Task<Result<IEnumerable<Domain.WeatherForecast>>> GetForecastAsync(ForecastRequestDto model)
    {
        return Task.FromResult(GetForecast(model));
    }

    private Result<IEnumerable<Domain.WeatherForecast>> GetForecast(ForecastRequestDto model)
    {
        switch (model.PostalCode)
        {
            case "NotFound":
                return Result.NotFound();
            case "Conflict":
                return Result.Conflict();
        }

        if (model.PostalCode.Length > 10)
        {
            return Result.Invalid(new List<ValidationError> {
                    new()
                    {
                        Identifier = nameof(model.PostalCode),
                        ErrorMessage = "PostalCode cannot exceed 10 characters." }
                });
        }

        if (string.IsNullOrWhiteSpace(model.PostalCode))
        {
            return Result.Invalid(new List<ValidationError> {
                    new()
                    {
                        Identifier = nameof(model.PostalCode),
                        ErrorMessage = "PostalCode is required" }
                    });
        }

        if (model.PostalCode == "55555")
        {
            return Result.Success(Enumerable.Range(1, 1)
                .Select(_ =>
                new Domain.WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = 0,
                    Summary = Summaries[0]
                }));
        }

        var rng = new Random();
        return Result.Success<IEnumerable<Domain.WeatherForecast>>(Enumerable.Range(1, 5)
            .Select(index => new Domain.WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
        .ToArray());
    }

    public Result<Domain.WeatherForecast> GetSingleForecast(ForecastRequestDto model)
    {
        switch (model.PostalCode)
        {
            case "NotFound":
                return Result.NotFound();
            case "Conflict":
                return Result.Conflict();
        }

        if (model.PostalCode.Length > 10)
        {
            return Result.Invalid(new List<ValidationError> {
                    new()
                    {
                        Identifier = nameof(model.PostalCode),
                        ErrorMessage = "PostalCode cannot exceed 10 characters."}
                });
        }

        if (model.PostalCode == "55555")
        {
            return Result.Success(
                new Domain.WeatherForecast
                {
                    Date = DateTime.Now,
                    TemperatureC = 0,
                    Summary = Summaries[0]
                });
        }

        var rng = new Random();
        return Result.Success(new Domain.WeatherForecast
        {
            Date = DateTime.Now.AddDays(rng.Next(1, 5)),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });
    }
}
namespace SF.App01.UseCases.WeatherForecast.Domain;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public static class WeatherForecastErrors
{
    public static readonly Error InvalidLocation = new("WeatherForecast.InvalidLocation", "Invalid location specified");

    public static readonly Error DataNotAvailable = new("WeatherForecast.DataNotAvailable", "Weather data not available for the specified location");

    public static readonly Error ServiceUnavailable = new("WeatherForecast.ServiceUnavailable", "Weather forecast service is currently unavailable");

    public static readonly Error InvalidDateRange = new("WeatherForecast.InvalidDateRange", "Specified date range is invalid");

    public static readonly Error UnexpectedError = new("WeatherForecast.UnexpectedError", "An unexpected error occurred while fetching weather data");
}

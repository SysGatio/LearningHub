namespace SF.App01.Core.UseCases.WeatherForecast.DTOs;

public class ForecastRequestDto
{
    [Required] 
    public string PostalCode { get; set; }
}
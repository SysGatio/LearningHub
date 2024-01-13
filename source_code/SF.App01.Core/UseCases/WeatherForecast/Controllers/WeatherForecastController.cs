namespace SF.App01.Core.UseCases.WeatherForecast.Controllers;
//https://github.com/ardalis/Result?tab=readme-ov-file
[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator) : ControllerBase
{
    [TranslateResultToActionResult]
    [HttpPost("Create")]
    public Task<Result<IEnumerable<Domain.WeatherForecast>>> CreateForecast([FromBody] NewForecastCommand model)
    {
        return mediator.Send(model);
    }

    public class NewForecastCommand : IRequest<Result<IEnumerable<Domain.WeatherForecast>>>
    {
        [Required] 
        public required string PostalCode { get; set; }
    }

    public class NewForecastHandler(WeatherService weatherService) : IRequestHandler<NewForecastCommand, Result<IEnumerable<Domain.WeatherForecast>>>
    {
        public Task<Result<IEnumerable<Domain.WeatherForecast>>> Handle(NewForecastCommand request, CancellationToken cancellationToken)
        {
            return weatherService.GetForecastAsync(new ForecastRequestDto { PostalCode = request.PostalCode });
        }
    }
}

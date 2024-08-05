using Api.Entities;
using MediatR;

namespace Api;

public class GetWeatherForecastHandler: IRequestHandler<GetWeatherForeCastQuery,WeatherForeCast[]>
{
    public Task<WeatherForeCast[]> 
        Handle(GetWeatherForeCastQuery request, CancellationToken cancellationToken)
    {
        string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForeCast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();

        return Task.FromResult(forecast);
    }
}
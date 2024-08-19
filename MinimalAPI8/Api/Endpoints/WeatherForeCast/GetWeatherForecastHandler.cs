namespace Api.Endpoints.WeatherForeCast;

public class GetWeatherForecastHandler : IRequestHandler<GetWeatherForeCastQuery, Models.WeatherForeCast[]>
{
    public Task<Models.WeatherForeCast[]>
        Handle(GetWeatherForeCastQuery request, CancellationToken cancellationToken)
    {
        string[] summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new Models.WeatherForeCast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();

        return Task.FromResult(forecast);
    }
}
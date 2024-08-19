namespace Api.Endpoints.WeatherForeCast;

public class WeatherForecastEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/weatherforecast");
        group.MapGet("", GetForecast);
    }
   
    private static async Task<Models.WeatherForeCast[]> GetForecast(ISender sender)
    {
        return await sender.Send(new GetWeatherForeCastQuery());
    }
}
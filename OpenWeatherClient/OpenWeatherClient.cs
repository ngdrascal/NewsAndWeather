namespace OpenWeatherClient;

public interface IOpenWeatherClient
{
    Task<dynamic?> GetDailyForecastAsync();
}

internal class OpenWeatherClient : IOpenWeatherClient
{
    public Task<dynamic?> GetDailyForecastAsync()
    {
        throw new NotImplementedException();
    }
}
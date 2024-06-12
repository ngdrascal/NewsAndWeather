namespace WeatherDataClients;

public interface IWeatherClient
{
    Location Location { get; }
    Task<WeatherData?> GetForecastsAsync();
}
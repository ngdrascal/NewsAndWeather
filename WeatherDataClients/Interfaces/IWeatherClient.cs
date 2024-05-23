namespace WeatherDataClients;

public interface IWeatherClient
{
    Location Location { get; }
    WeatherData? GetForecasts();
    Task<WeatherData?> GetForecastsAsync();
}
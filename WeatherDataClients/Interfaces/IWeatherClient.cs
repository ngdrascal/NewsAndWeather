namespace WeatherDataClients;

public interface IWeatherClient
{
    Task<WeatherData?> GetForecastsAsync();
}
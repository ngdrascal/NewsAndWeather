namespace TomorrowIOClient;

public interface ITomorrowIoClient
{
    Task<dynamic?> GetCurrentWeatherAsync();
    Task<dynamic?> GetHourlyForecastAsync();
    Task<dynamic?> GetDailyForecastAsync();
}
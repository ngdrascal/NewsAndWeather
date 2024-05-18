using System.Dynamic;

namespace WeatherDataClients.TomorrowIO;

public interface ITomorrowIoClient
{
    Task<dynamic?> GetCurrentWeatherAsync();
    Task<dynamic?> GetHourlyForecastAsync();
    Task<dynamic?> GetDailyForecastAsync();
}
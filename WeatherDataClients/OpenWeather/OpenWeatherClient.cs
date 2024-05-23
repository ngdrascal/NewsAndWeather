using System.Dynamic;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace WeatherDataClients.OpenWeather;

public class OpenWeatherClient : IWeatherClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly Location _location;
    private readonly TimeSpan _cacheDuration;
    private WeatherData? _cache;
    private DateTime _lastCacheRefresh;

    public OpenWeatherClient(HttpClient httpClient, string apiKey,
        Location location, TimeSpan cacheDuration)
    {
        var clientKey = location.Name;
        // _httpClient = httpClientFactory.CreateClient(clientKey);
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _apiKey = apiKey;
        _location = location;
        _cacheDuration = cacheDuration;
        _cache = null;
        _lastCacheRefresh = DateTime.MinValue;
    }

    public Location Location => _location;

    public WeatherData? GetForecasts()
    {
        return GetForecastsAsync().Result;
    }

    public async Task<WeatherData?> GetForecastsAsync()
    {
        if (_lastCacheRefresh + _cacheDuration > DateTime.Now)
            return _cache;

        var rawData = await GetWeatherDataAsync();

        if (rawData == null)
            return _cache ?? null;

        _cache = MapData(rawData);
        _lastCacheRefresh = DateTime.Now;
        return _cache;
    }

    private WeatherData MapData(dynamic openWeather)
    {
        var currentConditions = new CurrentConditions
        {
            DateAndTime = DateTimeOffset.FromUnixTimeSeconds(openWeather.current.dt),
            Temperature = openWeather.current.temp,
            FeelsLike = openWeather.current.feels_like,
            Pressure = (int)openWeather.current.pressure,
            Humidity = (int)openWeather.current.humidity,
            DewPoint = openWeather.current.dew_point,
            UltraVioletIndex = openWeather.current.uvi,
            Clouds = (int)openWeather.current.clouds,
            Visibility = (int)openWeather.current.visibility,
            WindSpeed = openWeather.current.wind_speed,
            WindDeg = (int)openWeather.current.wind_deg,
            // WindGust = openWeather.current["wind_gust"]
        };

        var hourlyForecasts = new List<HourlyForecast>();
        foreach (var hour in openWeather.hourly)
        {
            var forecast = new HourlyForecast
            {
                Temperature = hour.temp,
                ProbabilityOfPrecipitation = hour.pop
            };

            hourlyForecasts.Add(forecast);
        }

        var dailyForecasts = new List<DailyForecast>();
        foreach (var day in openWeather.daily)
        {
            var dailyForecast = new DailyForecast
            {
                Date = DateTimeOffset.FromUnixTimeSeconds(day.dt),
                HighTemp = day.temp.max,
                LowTemp = day.temp.min
            };

            dailyForecasts.Add(dailyForecast);
        }

        return new WeatherData(
            _location.Name,
             currentConditions,
             hourlyForecasts.ToArray(),
             dailyForecasts.ToArray()
        );
    }

    private async Task<dynamic?> GetWeatherDataAsync()
    {
        var url = new StringBuilder();
        url.Append("https://api.openweathermap.org");
        url.Append("/data/3.0/onecall?");
        url.Append("lat=");
        url.Append(_location.Latitude);
        url.Append("&lon=");
        url.Append(_location.Longitude);
        url.Append("&exclude=");
        url.Append("minutely");
        url.Append("&units=imperial");
        url.Append("&appid=");
        url.Append(_apiKey);

        var response = await _httpClient.GetAsync(url.ToString());
        response.EnsureSuccessStatusCode();
        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<ExpandoObject>(json);
        return data;
    }
}
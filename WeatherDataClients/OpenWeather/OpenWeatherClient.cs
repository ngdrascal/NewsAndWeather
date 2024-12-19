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

    public OpenWeatherClient(IHttpClientFactory httpClientFactory, string apiKey,
        Location location, TimeSpan cacheDuration)
    {
        _httpClient = httpClientFactory.CreateClient(location.Name);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _apiKey = apiKey;
        _location = location;
        _cacheDuration = cacheDuration;
        _cache = null;
        _lastCacheRefresh = DateTime.MinValue;
    }

    public Location Location => _location;

    public async Task<WeatherData?> GetForecastsAsync()
    {
        if (_lastCacheRefresh + _cacheDuration > DateTime.Now)
        {
            Console.WriteLine($"[{DateTime.Now}] OpenWeatherClient: reading from cache");
            return _cache;
        }

        Console.WriteLine($"[{DateTime.Now}] OpenWeatherClient: calling API");
        var rawData = await GetWeatherDataAsync();

        if (rawData == null)
            return _cache ?? null;

        _cache = MapData(rawData);
        _lastCacheRefresh = DateTime.Now;
        return _cache;
    }

    private WeatherData MapData(OpenWeatherResponse openWeather)
    {
        var currentConditions = new CurrentConditions
        {
            DateAndTime = DateTimeOffset.FromUnixTimeSeconds(openWeather.Current.Dt),
            Temperature = openWeather.Current.Temp,
            FeelsLike = openWeather.Current.FeelsLike,
            Pressure = openWeather.Current.Pressure,
            Humidity = openWeather.Current.Humidity,
            UltraVioletIndex = openWeather.Current.Uvi,
            Visibility = openWeather.Current.Visibility,
            WindSpeed = openWeather.Current.WindSpeed,
            WindDeg = openWeather.Current.WindDeg,
            Pop = openWeather.Daily[0].Pop,
            Weather = new Weather
            {
                Id = openWeather.Current.Weather[0].Id,
                Main = openWeather.Current.Weather[0].Main,
                Description = openWeather.Current.Weather[0].Description,
                Icon = openWeather.Current.Weather[0].Icon
            }
        };

        var hourlyForecasts = openWeather.Hourly.Select(hour => new HourlyForecast
        {
            Temperature = hour.Temp,
            ProbabilityOfPrecipitation = hour.Pop
        }).ToList();

        var dailyForecasts = openWeather.Daily.Select(day => new DailyForecast
        {
            Date = DateTimeOffset.FromUnixTimeSeconds(day.Dt),
            HighTemp = day.Temp.Max,
            LowTemp = day.Temp.Min,
            Weather = new Weather
            {
                Id = day.Weather[0].Id,
                Main = day.Weather[0].Main,
                Description = day.Weather[0].Description,
                Icon = day.Weather[0].Icon
            }
        }).ToList();

        return new WeatherData(
            _location.Name,
            DateTime.Now,
            currentConditions,
            hourlyForecasts.ToArray(),
            dailyForecasts.ToArray()
        );
    }

    /*
    private WeatherData MapData(dynamic openWeather)
    {
        var currentConditions = new CurrentConditions
        {
            DateAndTime = DateTimeOffset.FromUnixTimeSeconds(openWeather.current.dt),
            Temperature = openWeather.current.temp,
            FeelsLike = openWeather.current.feels_like,
            Pressure = (int)openWeather.current.pressure,
            Humidity = (int)openWeather.current.humidity,
            // DewPoint = openWeather.current.dew_point,
            UltraVioletIndex = openWeather.current.uvi,
            // Clouds = (int)openWeather.current.clouds,
            Visibility = (int?)openWeather.current.visibility,
            WindSpeed = openWeather.current.wind_speed,
            WindDeg = (int)openWeather.current.wind_deg,
            // WindGust = openWeather.current["wind_gust"],
            Pop = openWeather.daily[0]?.pop,
            WeatherDescriptor = new WeatherDescriptor
            {
                Id = openWeather.current.weather[0].id,
                Main = openWeather.current.weather[0].main,
                Description = openWeather.current.weather[0].description,
                Icon = openWeather.current.weather[0].icon
            }
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
                LowTemp = day.temp.min,
                WeatherDescriptor = new WeatherDescriptor
                {
                    Id = day.weather[0].id,
                    Main = day.weather[0].main,
                    Description = day.weather[0].description,
                    Icon = day.weather[0].icon
                }
            };

            dailyForecasts.Add(dailyForecast);
        }

        return new WeatherData(
            _location.Name,
            DateTime.Now,
            currentConditions,
            hourlyForecasts.ToArray(),
            dailyForecasts.ToArray()
        );
    }
    */

    private async Task<OpenWeatherResponse?> GetWeatherDataAsync()
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

        try
        {
            var response = await _httpClient.GetAsync(url.ToString());
            response.EnsureSuccessStatusCode();
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<OpenWeatherResponse>(json);
            return data;
        }
        catch(Exception)
        {
            return null;
        }
    }
}

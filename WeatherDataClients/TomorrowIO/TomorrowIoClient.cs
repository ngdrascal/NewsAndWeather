﻿using System.Net;
using Newtonsoft.Json;

namespace WeatherDataClients.TomorrowIO;

public class TomorrowIoClient : ITomorrowIoClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _location;

    public TomorrowIoClient(IHttpClientFactory httpClientFactory, string apiKey, Location location)
    {
        var clientKey = location.Name;
        _httpClient = httpClientFactory.CreateClient(clientKey);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _apiKey = apiKey;
        _location = $"{location.Latitude},{location.Longitude}";
    }

    public async Task<dynamic?> GetCurrentWeatherAsync()
    {
        var url =
            $"https://api.tomorrow.io/v4/weather/realtime?location={_location}&apikey={_apiKey}";

        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var dynoContent = JsonConvert.DeserializeObject<dynamic>(content);
        return dynoContent;
    }

    public async Task<dynamic?> GetHourlyForecastAsync()
    {
        return await GetForecastAsync("1h");
    }

    public async Task<dynamic?> GetDailyForecastAsync()
    {
        return await GetForecastAsync("1d");
    }

    private async Task<dynamic?> GetForecastAsync(string interval)
    {
        var url =
            $"https://api.tomorrow.io/v4/weather/forecast?location={_location}&apikey={_apiKey}&timestep={interval}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        if (response.StatusCode != HttpStatusCode.OK)
            return null;

        var content = await response.Content.ReadAsStringAsync();

        var dynoContent = JsonConvert.DeserializeObject<dynamic>(content);
        return dynoContent;
    }
}
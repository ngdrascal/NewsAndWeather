using ResilientHttpClient;
using System.Diagnostics.CodeAnalysis;
using WeatherDataClients.OpenWeather;

namespace WeatherDataClients.Tests;

[ExcludeFromCodeCoverage]

public class OpenWeatherClientTests
{
    [Test]
    public async Task Test1()
    {
        // ARRANGE:
        var apiKey = Environment.GetEnvironmentVariable("Weather:ApiKey");
        Assert.That(apiKey, Is.Not.Null);

        var httpClientFactory = new ResilientHttpClientFactory();
        var location = new Location("Central Park", 40.7841, -73.9657);
        var cacheDuration = TimeSpan.FromMinutes(1);
        var client = new OpenWeatherClient(httpClientFactory, apiKey, location, cacheDuration);

        // ACT:
        var weatherData = await client.GetAllForecastData();

        // ASSERT
        Assert.That(weatherData, Is.Not.Null);
    }
}
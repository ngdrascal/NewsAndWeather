using Microsoft.Extensions.Configuration;
using ResilientHttpClient;
using WeatherDataClients.OpenWeather;

namespace WeatherDataClients.Tests;

public class OpenWeatherClientTests
{
    [Test]
    public async Task Test1()
    {
        // ARRANGE:
        var configBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Weather:CacheDuration"] = "1",
                ["Weather:Locations:Name"] = "Central Park",
                ["Weather:Locations:Latitude"] = "40.7841",
                ["Weather:Locations:"] = "-73.9657"
            })
            .AddEnvironmentVariables();
        var config = configBuilder.Build();


        var httpClientFactory = new ResilientHttpClientFactory();
        var apiKey = Environment.GetEnvironmentVariable("Weather:ApiKey");
        var location = new Location("Central Park", 40.7841, -73.9657);
        var cacheDuration = TimeSpan.FromMinutes(1);
        var client = new OpenWeatherClient(httpClientFactory, apiKey, location, cacheDuration);

        // ACT:
        var weatherData = await client.GetAllForecastData();

        // ASSERT
        Assert.That(weatherData, Is.Not.Null);
    }
}
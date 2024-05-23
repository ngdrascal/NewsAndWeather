using ResilientHttpClient;
using System.Diagnostics.CodeAnalysis;
using WeatherDataClients.OpenWeather;
using Microsoft.Extensions.Configuration;
using System.Globalization;

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

        // var httpClientFactory = new ResilientHttpClientFactory();
        var httpClient = new HttpClient();
        var location = new Location("Central Park", 40.7841, -73.9657);
        var cacheDuration = TimeSpan.FromMinutes(1);
        var client = new OpenWeatherClient(httpClient, apiKey, location, cacheDuration);

        // ACT:
        var weatherData = await client.GetForecastsAsync();

        // ASSERT
        Assert.That(weatherData, Is.Not.Null);
    }

    [Test]
    public async Task Test2()
    {
        // ARRANGE:

        var configuration = BuildConfiguration();
        var httpClientFactory = new ResilientHttpClientFactory();
        var apiKeyProvider = new ApiKeyProvider(configuration);
        var clientCollection = new WeatherClientCollection(httpClientFactory, apiKeyProvider.GetApiKey());

        var locProvider = new LocationProvider(configuration);
        var locations = locProvider.GetAll();
        foreach (var loc in locations)
            clientCollection.AddClient(loc.Name, loc, TimeSpan.FromMinutes(5));
        var clients = clientCollection.GetAll().ToList();

        // ACT:
        WeatherData? weatherData = null;
        foreach (var client in clients)
            weatherData = await client.GetForecastsAsync();

        // ASSERT:
        Assert.That(clients.Count, Is.EqualTo(2));
        Assert.That(weatherData, Is.Not.Null);
    }

    private IConfiguration BuildConfiguration()
    {
        const string location1Name = "Central Park";
        const double location1Latitude = 40.7841;
        const double location1Longitude = -73.9657;

        const string location2Name = "The Parthenon";
        const double location2Latitude = 36.1503;
        const double location2Longitude = -86.8133;

        var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Weather:ApiKey"] = "<your_API_key_goes_here>",
                    ["Weather:CacheDuration"] = "1",
                    ["Weather:Locations:0:Name"] = location1Name,
                    ["Weather:Locations:0:Latitude"] = location1Latitude.ToString(CultureInfo.InvariantCulture),
                    ["Weather:Locations:0:Longitude"] = location1Longitude.ToString(CultureInfo.InvariantCulture),
                    ["Weather:Locations:1:Name"] = location2Name,
                    ["Weather:Locations:1:Latitude"] = location2Latitude.ToString(CultureInfo.InvariantCulture),
                    ["Weather:Locations:1:Longitude"] = location2Longitude.ToString(CultureInfo.InvariantCulture)
                })
                .AddEnvironmentVariables();

        return configBuilder.Build();
    }
}

using ResilientHttpClient;
using WeatherDataClients.TomorrowIO;

namespace WeatherDataClients.Tests;

public class TomorrowIoClientTests
{
    [Test]
    public async Task Test1()
    {
        // ARRANGE:
        var httpClientFactory = new ResilientHttpClientFactory();
        var apiKey = "";
        var location = new Location("Central Park", 40.7841, -73.9657);
        ITomorrowIoClient client = new TomorrowIO.TomorrowIoClient(httpClientFactory, apiKey, location);

        // ACT:
        var r = await client.GetCurrentWeatherAsync();

        // ASSERT
        Assert.Pass();
    }
}
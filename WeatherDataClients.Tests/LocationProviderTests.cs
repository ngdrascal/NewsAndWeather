using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace WeatherDataClients.Tests;

[ExcludeFromCodeCoverage]
public class LocationProviderTests
{
    private const string Location1Name = "Central Park";
    private const double Location1Latitude = 40.7841;
    private const double Location1Longitude = -73.9657;

    private const string Location2Name = "The Parthenon";
    private const double Location2Latitude = 36.1503;
    private const double Location2Longitude = -86.8133;

    private IConfiguration BuildLocationConfiguration()
    {
        var configBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Weather:ApiKey"] = "<your_API_key_goes_here>",
                ["Weather:CacheDuration"] = "1",
                ["Weather:Locations:0:Name"] = Location1Name,
                ["Weather:Locations:0:Latitude"] = Location1Latitude.ToString(CultureInfo.InvariantCulture),
                ["Weather:Locations:0:Longitude"] = Location1Longitude.ToString(CultureInfo.InvariantCulture),
                ["Weather:Locations:1:Name"] = Location2Name,
                ["Weather:Locations:1:Latitude"] = Location2Latitude.ToString(CultureInfo.InvariantCulture),
                ["Weather:Locations:1:Longitude"] = Location2Longitude.ToString(CultureInfo.InvariantCulture)
            });

        return configBuilder.Build();
    }

    [Test]
    public void Test()
    {
        var config = BuildLocationConfiguration();

        var locationSection = config.GetRequiredSection("Weather:Locations");
        var locationArray = locationSection.GetChildren();

        var locations = locationArray.Select(configSect =>
        {
            var name = configSect.GetValue<string>("Name");
            var latitude = configSect.GetValue<double>("Latitude");
            var longitude = configSect.GetValue<double>("Longitude");

            return new Location(name, latitude, longitude);
        }).ToArray();

        Assert.That(locations.Length, Is.EqualTo(2));
    }

    // GIVEN: Configuration data will multiple locations
    // WHEN: Get() is called with a location name that existing in the configuration
    // THEN: The location matching the name is returned
    [Test]
    public void GetReturnsLocation()
    {
        // ARRANGE:
        var config = BuildLocationConfiguration();
        var locationProvider = new LocationProvider(config);

        // ACT:
        var location = locationProvider.Get(Location1Name);

        // ASSERT:
        Assert.That(location, Is.Not.Null);
        Assert.That(location.Name, Is.EqualTo(Location1Name));
        Assert.That(location.Latitude, Is.EqualTo(Location1Latitude));
        Assert.That(location.Longitude, Is.EqualTo(Location1Longitude));
    }

    // GIVEN: Configuration data will multiple locations
    // WHEN: GetAll() is called
    // THEN: All locations are returned
    [Test]
    public void GetAllReturnsAllLocations()
    {
        // ARRANGE:
        var config = BuildLocationConfiguration();

        var locationProvider = new LocationProvider(config);

        // ACT:
        var locations = locationProvider.GetAll().ToList();

        // ASSERT:
        Assert.That(locations.Count, Is.EqualTo(2));
    }
}
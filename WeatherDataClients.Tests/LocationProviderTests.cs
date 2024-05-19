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

    private IConfiguration BuildConfiguration()
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

    private IConfiguration BuildConfiguration(string? name, string? latitude, string? longitude)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Weather:ApiKey"] = "<your_API_key_goes_here>",
                ["Weather:CacheDuration"] = "1",
                ["Weather:Locations:0:Name"] = name,
                ["Weather:Locations:0:Latitude"] = latitude,
                ["Weather:Locations:0:Longitude"] = longitude
            });

        return configBuilder.Build();
    }

    // GIVEN: Configuration with a null or blank location name
    // WHEN: GetAll() is called
    // THEN: An InvalidOperationException is thrown
    [TestCase(null)]
    [TestCase("")]
    public void NameIsRequired(string? name)
    {
        var config = BuildConfiguration(name, "0.0", "0.0");
        var locationProvider = new LocationProvider(config);
        var expectedMessage = "Location name is required";

        // ACT:

        // ASSERT:
        Assert.Throws<InvalidOperationException>(() => locationProvider.GetAll(), expectedMessage);
    }

    // GIVEN: Configuration with a null or blank value for latitude
    // WHEN: GetAll() is called
    // THEN: An InvalidOperationException is thrown
    [TestCase("TestNull", null)]
    [TestCase("TestEmpty", "")]
    public void LatitudeIsRequired(string name, string? latitude)
    {
        var config = BuildConfiguration(name, latitude, "0.0");
        var locationProvider = new LocationProvider(config);
        var expectedMessage = "Location latitude is required for " + name;

        // ACT:

        // ASSERT:
        Assert.Throws<InvalidOperationException>(() => locationProvider.GetAll(), expectedMessage);
    }

    // GIVEN: Configuration with a null or blank value for longitude
    // WHEN: GetAll() is called
    // THEN: An InvalidOperationException is thrown
    [TestCase("TestNull", null)]
    [TestCase("TestEmpty", "")]
    public void LongitudeIsRequired(string name, string? longitude)
    {
        var config = BuildConfiguration(name, "0.0", longitude);
        var locationProvider = new LocationProvider(config);
        var expectedMessage = "Location longitude is required for " + name;

        // ACT:

        // ASSERT:
        Assert.Throws<InvalidOperationException>(() => locationProvider.GetAll(), expectedMessage);
    }


    // GIVEN: Configuration with an
    // WHEN: GetAll() is called
    // THEN: An InvalidOperationException is thrown
    [Test]
    public void LatitudeIsRequired()
    {
        var config = BuildConfiguration("Home", "null", "76.54321");
        var locationProvider = new LocationProvider(config);

        // ACT:

        // ASSERT:
        Assert.Throws<InvalidOperationException>(() => locationProvider.GetAll());
    }

    // GIVEN: Configuration data will multiple locations
    // WHEN: Get() is called with a location name that existing in the configuration
    // THEN: The location matching the name is returned
    [Test]
    public void GetReturnsLocation()
    {
        // ARRANGE:
        var config = BuildConfiguration();
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
        var config = BuildConfiguration();

        var locationProvider = new LocationProvider(config);

        // ACT:
        var locations = locationProvider.GetAll().ToList();

        // ASSERT:
        Assert.That(locations.Count, Is.EqualTo(2));
    }
}
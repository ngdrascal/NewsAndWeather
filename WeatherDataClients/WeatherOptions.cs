#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace WeatherDataClients;

public class WeatherOptions
{
    public const string SectionName = "Weather";

    public string ApiKey { get; set; }
    public int CacheDuration { get; set; }
    public LocationOption[] Locations { get; set; }
}

public class LocationOption
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
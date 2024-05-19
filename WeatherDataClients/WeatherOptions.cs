#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace WeatherDataClients;

public sealed class WeatherOptions
{
    public const string SectionName = "Weather";

    public required string ApiKey { get; set; }
    public required int CacheDuration { get; set; }
    public required LocationsOption[] Locations { get; set; }
}

public sealed class LocationsOption
{
    public required string Name { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
}
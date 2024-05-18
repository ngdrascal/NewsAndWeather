namespace WeatherDataClients;

public class Location(string name, double latitude, double longitude)
{
    public string Name { get; } = name;
    public double Latitude { get; } = latitude;
    public double Longitude { get; } = longitude;
}
namespace NewsAndWeather.Models;

public class Position(double latitude, double longitude)
{
    public double Latitude { get; } = latitude;
    public double Longitude { get; } = longitude;
}
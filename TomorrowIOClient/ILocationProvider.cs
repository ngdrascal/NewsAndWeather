namespace TomorrowIOClient;

public interface ILocationProvider
{
    double Latitude { get; }
    double Longitude { get; }
}
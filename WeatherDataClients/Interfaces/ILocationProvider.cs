namespace WeatherDataClients;

public interface ILocationProvider
{
    Location? Get(string name);
    IEnumerable<Location> GetAll();
}
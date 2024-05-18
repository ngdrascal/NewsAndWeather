namespace WeatherDataClients;

public interface IWeatherClientCache
{
    public void AddClient(string name, Location location, TimeSpan cacheDuration);
    public IWeatherClient GetClient(string name);
}
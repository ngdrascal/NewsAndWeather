namespace WeatherDataClients;

public interface IWeatherClientCollection
{
    public void AddClient(string name, Location location, TimeSpan cacheDuration);
    public IWeatherClient GetClient(string name);
    public IEnumerable<IWeatherClient> GetAll();
}
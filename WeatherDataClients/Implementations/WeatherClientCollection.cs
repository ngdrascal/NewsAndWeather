using WeatherDataClients.OpenWeather;

namespace WeatherDataClients;

public class WeatherClientCollection : IWeatherClientCollection
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey;
    private readonly IDictionary<string, IWeatherClient> _clients = new Dictionary<string, IWeatherClient>();

    public WeatherClientCollection(IHttpClientFactory httpClientFactory, string apiKey)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException($"Parameter {nameof(apiKey)} cannot be null or empty", nameof(apiKey));

        _apiKey = apiKey;
    }

    public void AddClient(Location location, TimeSpan cacheDuration)
    {
        ArgumentNullException.ThrowIfNull(location, nameof(location));

        if (string.IsNullOrWhiteSpace(location.Name))
            throw new ArgumentException($"Parameter {nameof(location)} cannot have a null or empty {nameof(location.Name)}",
                nameof(location.Name));

        var name = location.Name.ToUpper();

        if (_clients.ContainsKey(name))
            return;

        var client = new OpenWeatherClient(_httpClientFactory, _apiKey, location, cacheDuration);
        _clients.Add(name, client);
    }

    public IWeatherClient GetClient(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"Parameter {nameof(name)} cannot be null or empty", nameof(name));

        return _clients[name.ToUpper()];
    }

    public IEnumerable<IWeatherClient> GetAll()
    {
        return _clients.Values;
    }
}
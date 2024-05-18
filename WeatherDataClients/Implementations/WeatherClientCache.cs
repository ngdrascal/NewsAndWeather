using WeatherDataClients.OpenWeather;

namespace WeatherDataClients;

public class WeatherClientCache : IWeatherClientCache
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey;
    private readonly IDictionary<string, IWeatherClient> _clients = new Dictionary<string, IWeatherClient>();

    public WeatherClientCache(IHttpClientFactory httpClientFactory, string apiKey)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException($"Parameter {nameof(apiKey)} cannot be null or empty", nameof(apiKey));

        _apiKey = apiKey;
    }

    public void AddClient(string name, Location location, TimeSpan cacheDuration)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException($"Parameter {nameof(name)} cannot be null or empty", nameof(name));

        if (location is null)
            throw new ArgumentNullException(nameof(location));

        name = name.ToUpper();

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

}
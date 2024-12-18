using Microsoft.Extensions.Configuration;

namespace WeatherDataClients;

public class ApiKeyProvider : IApiKeyProvider
{
    private readonly IConfiguration _configuration;
    private const string KeyName = "Weather:ApiKey";

    public ApiKeyProvider(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string GetApiKey()
    {
        var apiKey = _configuration[KeyName];
        if (apiKey is null)
            throw new InvalidOperationException($"Missing '{KeyName}' configuration");

        return apiKey;
    }
}
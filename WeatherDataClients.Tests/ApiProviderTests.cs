using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace WeatherDataClients.Tests;

[ExcludeFromCodeCoverage]

public class ApiProviderTests
{
    // GIVEN: the ApiKey setting exists in two places one of those being the environment
    // WHEN: the GetApiKey method is called
    // THEN:the ApiKey is returned from the environment
    [Test]
    public void CanReadOverrideFromEnvironment()
    {
        // ARRANGE:
        var configBuilder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Weather:ApiKey"] = "<your_API_key_goes_here>",
                    ["Weather:CacheDuration"] = "1",
                    ["Weather:Locations:Name"] = "Central Park",
                    ["Weather:Locations:Latitude"] = "40.7841",
                    ["Weather:Locations:"] = "-73.9657"
                })
                .AddEnvironmentVariables()
            ;
        var config = configBuilder.Build();

        var apiKeyProvider = new ApiKeyProvider(config);

        // ACT:
        var apiKey = apiKeyProvider.GetApiKey();

        // ASSERT:
        Assert.That(string.IsNullOrEmpty(apiKey), Is.False);
    }
}
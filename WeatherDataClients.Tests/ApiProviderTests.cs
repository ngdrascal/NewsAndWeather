using Microsoft.Extensions.Configuration;

namespace WeatherDataClients.Tests
{
    public class ApiProviderTests
    {
        // GIVEN: 
        // WHEN:
        // THEN:
        [Test]
        public void CanReadFromEnvironment()
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
}

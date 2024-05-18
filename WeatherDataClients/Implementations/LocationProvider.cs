using Microsoft.Extensions.Configuration;

namespace WeatherDataClients
{
    public class LocationProvider : ILocationProvider
    {
        private readonly IConfiguration _configuration;

        public LocationProvider(IConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public Location? Get(string name)
        {
            var options = _configuration.GetSection(WeatherOptions.SectionName).Get<WeatherOptions>();
            if (options is null)
                throw new InvalidOperationException($"Missing '{WeatherOptions.SectionName}' section in configuration");

            if (options.Locations is null)
                throw new InvalidOperationException($"Missing '{WeatherOptions.SectionName}:Locations' section in configuration");

            var locOption = options.Locations.FirstOrDefault(l => l.Name == name);
            if (locOption is null)
                return null;

            return new Location(locOption.Name, locOption.Latitude, locOption.Longitude);
        }

        public IEnumerable<Location> GetAll()
        {
            var options = _configuration.GetSection(WeatherOptions.SectionName).Get<WeatherOptions>();
            if (options is null)
                throw new InvalidOperationException($"Missing '{WeatherOptions.SectionName}' section in configuration");

            if (options.Locations is null)
                throw new InvalidOperationException($"Missing '{WeatherOptions.SectionName}:Locations' section in configuration");

            return options.Locations.Select(l => new Location(l.Name, l.Latitude, l.Longitude));
        }
    }
}

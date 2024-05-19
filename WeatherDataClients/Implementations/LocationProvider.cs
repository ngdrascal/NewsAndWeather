using Microsoft.Extensions.Configuration;

namespace WeatherDataClients
{
    public class LocationProvider : ILocationProvider
    {
        private readonly IConfiguration _configuration;

        private const string LocationSectionName = "Weather:Locations";

        public LocationProvider(IConfiguration configuration)
        {
            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public Location? Get(string name)
        {
            var locations = GetAll();
            return locations.FirstOrDefault(loc => loc.Name == name);
        }

        public IEnumerable<Location> GetAll()
        {
            var locationSection = _configuration.GetRequiredSection(LocationSectionName);
            if (locationSection is null)
                throw new InvalidOperationException($"Missing '{LocationSectionName}' section in configuration");

            var locationArray = locationSection.GetChildren().ToArray();
            if (locationArray.Length == 0)
                throw new InvalidOperationException("No locations found in configuration");

            var locations = locationArray.Select(configSect =>
            {
                var name = configSect.GetValue<string>("Name");
                if (string.IsNullOrWhiteSpace(name))
                    throw new InvalidOperationException("Location name is required");

                var latitude = configSect.GetValue("Latitude", double.MaxValue);
                if (Math.Abs(latitude - double.MaxValue) < 0.1)
                    throw new InvalidOperationException($"Location latitude is required for {name}");
                if (Math.Abs(latitude) > 90.0)
                    throw new InvalidOperationException($"Latitude for location {name} is outside the range of -90 to +90");

                var longitude = configSect.GetValue("Longitude", double.MaxValue);
                if (Math.Abs(longitude - double.MaxValue) < 0.1)
                    throw new InvalidOperationException($"Location longitude is required for {name}");
                if (Math.Abs(latitude) > 90.0)
                    throw new InvalidOperationException($"Longitude for location {name} is outside the range of -180 to +180");

                return new Location(name, latitude, longitude);
            }).ToArray();

            return locations;
        }
    }
}

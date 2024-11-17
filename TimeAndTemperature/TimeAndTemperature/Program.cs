using TimeAndTemperature.Components;
using WeatherDataClients;

namespace TimeAndTemperature
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();

            // Add services to the container.
            builder.Services
                .AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddHttpClient("default");

            builder.Services.AddSingleton<IApiKeyProvider, ApiKeyProvider>();
            builder.Services.AddSingleton<ILocationProvider, LocationProvider>();
            builder.Services.AddSingleton<IWeatherClientCollection>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var apiKeyProvider = sp.GetRequiredService<IApiKeyProvider>();
                var clientCache = new WeatherClientCollection(httpClientFactory, apiKeyProvider.GetApiKey());

                var locProvider = sp.GetRequiredService<ILocationProvider>();
                var locations = locProvider.GetAll();

                foreach (var loc in locations)
                    clientCache.AddClient(loc, TimeSpan.FromMinutes(5));

                return clientCache;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios,
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(TimeAndTemperature.Client._Imports).Assembly);

            app.Run();
        }
    }
}

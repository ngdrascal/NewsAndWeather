namespace WeatherDataClients;

public class WeatherData
{
    public WeatherData(
        string locationName,
        CurrentConditions currentConditions,
        HourlyForecast[] hourlyForecasts,
        DailyForecast[] dailyForecasts)
    {
        if (string.IsNullOrEmpty(locationName))
            throw new ArgumentException("Value cannot be null or empty.", nameof(locationName));
        ArgumentNullException.ThrowIfNull(currentConditions, nameof(currentConditions));
        ArgumentNullException.ThrowIfNull(hourlyForecasts, nameof(hourlyForecasts));
        ArgumentNullException.ThrowIfNull(dailyForecasts,nameof(dailyForecasts));

        LocationName = locationName;
        CurrentConditions = currentConditions;
        HourlyForecasts = hourlyForecasts;
        DailyForecasts = dailyForecasts;
    }

    public string LocationName { get; }
    public CurrentConditions CurrentConditions { get; }
    public HourlyForecast[] HourlyForecasts { get; }
    public DailyForecast[] DailyForecasts { get; }
}

public class CurrentConditions
{
    public DateTimeOffset DateAndTime { get; set; }
    public double Temperature { get; set; }
    public double FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public double DewPoint { get; set; }
    public double UltraVioletIndex { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    public double WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public double WindGust { get; set; }
    public double Pop { get; set; }
    public Weather Weather { get; set; }
}

public class Weather
{
    public long Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class DailyForecast
{
    public DateTimeOffset Date { get; set; }
    public double HighTemp { get; set; }
    public double LowTemp { get; set; }
    public Weather Weather { get; set; }
}

public class HourlyForecast
{
    public double Temperature { get; set; }
    public double ProbabilityOfPrecipitation { get; set; }
}

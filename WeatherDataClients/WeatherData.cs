namespace WeatherDataClients;

public class WeatherData
{
    public WeatherData(
        CurrentConditions currentConditions,
        HourlyForecast[] hourlyForecasts,
        DailyForecast[] dailyForecasts)
    {
        if (currentConditions is null)
            throw new ArgumentNullException(nameof(currentConditions));
        if (hourlyForecasts is null)
            throw new ArgumentNullException(nameof(hourlyForecasts));
        if (dailyForecasts is null)
            throw new ArgumentNullException(nameof(dailyForecasts));

        CurrentConditions = currentConditions;
        HourlyForecasts = hourlyForecasts;
        DailyForecasts = dailyForecasts;
    }

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
}

public class DailyForecast
{
    public DateTimeOffset Date { get; set; }
    public double HighTemp { get; set; }
    public double LowTemp { get; set; }
}

public class HourlyForecast
{
    public double Temperature { get; set; }
    public double ProbabilityOfPrecipitation { get; set; }
}

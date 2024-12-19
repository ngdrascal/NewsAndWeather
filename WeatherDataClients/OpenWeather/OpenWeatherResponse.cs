namespace WeatherDataClients.OpenWeather;

/*
public class OpenWeatherResponse
{
    public float Lat { get; set; }
    public float Lon { get; set; }
    public string Timezone { get; set; }
    public int TimezoneOffset { get; set; }
    public Current Current { get; set; }
    public Minutely[] Minutely { get; set; }
    public Hourly[] Hourly { get; set; }
    public Daily[] Daily { get; set; }
    public Alert[] Alerts { get; set; }
}

public class Current
{
    public int Dt { get; set; }
    public int Sunrise { get; set; }
    public int Sunset { get; set; }
    public float Temp { get; set; }
    public float FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public float DewPoint { get; set; }
    public float Uvi { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    public float WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public float WindGust { get; set; }
    public WeatherDescriptor[] WeatherDescriptor { get; set; }
}

public class WeatherDescriptor
{
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class Minutely
{
    public int Dt { get; set; }
    public int Precipitation { get; set; }
}

public class Hourly
{
    public int Dt { get; set; }
    public float Temp { get; set; }
    public float FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public float DewPoint { get; set; }
    public int Uvi { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    public float WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public float WindGust { get; set; }
    public Weather1[] WeatherDescriptor { get; set; }
    public float Pop { get; set; }
}

public class Weather1
{
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class Daily
{
    public int Dt { get; set; }
    public int Sunrise { get; set; }
    public int Sunset { get; set; }
    public int Moonrise { get; set; }
    public int Moonset { get; set; }
    public float MoonPhase { get; set; }
    public string Summary { get; set; }
    public Temp Temp { get; set; }
    public FeelsLike FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public float DewPoint { get; set; }
    public float WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public float WindGust { get; set; }
    public WeatherInfo[] WeatherDescriptor { get; set; }
    public int Clouds { get; set; }
    public float Pop { get; set; }
    public float Rain { get; set; }
    public float Uvi { get; set; }
}

public class Temp
{
    public float Day { get; set; }
    public float Min { get; set; }
    public float Max { get; set; }
    public float Night { get; set; }
    public float Eve { get; set; }
    public float Morn { get; set; }
}

public class FeelsLike
{
    public float Day { get; set; }
    public float Night { get; set; }
    public float Eve { get; set; }
    public float Morn { get; set; }
}

public class WeatherInfo
{
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class Alert
{
    public string SenderName { get; set; }
    public string Event { get; set; }
    public int Start { get; set; }
    public int End { get; set; }
    public string Description { get; set; }
    public object[] Tags { get; set; }
}
*/

public class OpenWeatherResponse
{
    public CurrentWeather Current { get; set; }
    public List<HourlyWeather> Hourly { get; set; }
    public List<DailyWeather> Daily { get; set; }
}

public class CurrentWeather
{
    public long Dt { get; set; }
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public double Uvi { get; set; }
    public int? Visibility { get; set; }
    public double WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public List<WeatherInfo> Weather { get; set; }
}

public class HourlyWeather
{
    public double Temp { get; set; }
    public double Pop { get; set; }
}

public class DailyWeather
{
    public long Dt { get; set; }
    public Temp Temp { get; set; }
    public double Pop { get; set; }

    public List<Weather> Weather { get; set; }
}

public class Temp
{
    public double Min { get; set; }
    public double Max { get; set; }
}

public class WeatherInfo
{
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

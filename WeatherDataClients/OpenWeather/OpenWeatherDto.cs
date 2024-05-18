/*
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

using System.Text.Json.Serialization;

namespace WeatherDataClients.OpenWeather;

public class OpenWeather
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Timezone { get; set; }
    [JsonPropertyName("timezone_offset")]
    public int Timezone_Offset { get; set; }
    public CurrentConditions Current { get; set; }
    public Hourly[] Hourly { get; set; }
    public Daily[] Daily { get; set; }
    public Alert[] Alerts { get; set; }
}

public class CurrentConditions
{
    public long Dt { get; set; }
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
    public double Temp { get; set; }
    [JsonPropertyName("Feels_Like")]
    public FeelsLike Feels_Like { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    [JsonPropertyName("dew_point")]
    public double Dew_Point { get; set; }
    public double UltraVioletIndex { get; set; }
    public int Clouds { get; set; }
    public int Visibility { get; set; }
    [JsonPropertyName("wind_speed")]
    public double Wind_Speed { get; set; }
    [JsonPropertyName("wind_deg")]
    public int Wind_Deg { get; set; }
    [JsonPropertyName("wind_gust")]
    public double Wind_Gust { get; set; }
    public Weather[] Weather { get; set; }
}

public class Hourly : CurrentConditions
{
    public double Pop { get; set; }
}

public class Daily
{
    public long Dt { get; set; }
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
    public long MoonRise { get; set; }
    public long MoonSet { get; set; }
    [JsonPropertyName("Moon_Phase")]
    public double Moon_Phase { get; set; }
    public string Summary { get; set; }
    public Temperature Temp { get; set; }
    [JsonPropertyName("Feels_Like")]
    public FeelsLike Feels_Like { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    [JsonPropertyName("Dew_Point")]
    public double Dew_Point { get; set; }
    [JsonPropertyName("Wind_Speed")]
    public double Wind_Speed { get; set; }
    [JsonPropertyName("Wind_Deg")]
    public int WindDeg { get; set; }
    [JsonPropertyName("Wind_Gust")]
    public double Wind_Gust { get; set; }
    public Weather[] Weather { get; set; }
    public int Clouds { get; set; }
    public double Pop { get; set; }
    public double Rain { get; set; }
    public double UltraVioletIndex { get; set; }
}

public class Temperature
{
    public double Day { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public double Night { get; set; }
    public double Eve { get; set; }
    public double Morn { get; set; }
}

public class FeelsLike
{
    public double Day { get; set; }
    public double Night { get; set; }
    public double Eve { get; set; }
    public double Morn { get; set; }
}   

public class Weather
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
    public long Start { get; set; }
    public long End { get; set; }
    public string Description { get; set; }
}
*/
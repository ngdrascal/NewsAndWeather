namespace OpenWeatherClient;

public class OpenWeather
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string Timezone { get; set; }
    public int Timezone_Offset { get; set; }
    public Daily[] Daily { get; set; }
}

public class Daily
{
    public long Dt { get; set; }
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
    public long Moonrise { get; set; }
    public long Moonset { get; set; }
    public double Moon_Phase { get; set; }
    public string Summary { get; set; }
    public Tempature Temp { get; set; }
    public FeelsLike Feels_Like { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
    public double Dew_Point { get; set; }
    public double Wind_Speed { get; set; }
    public int Wind_Deg { get; set; }
    public double Wind_Gust { get; set; }
    public Weather[] Weather { get; set; }
    public int Clouds { get; set; }
    public int Pop { get; set; }
    public double Rain { get; set; }
    public double Uvi { get; set; }
}

public class Tempature
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
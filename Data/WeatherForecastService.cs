using System;
using System.Net.Http;
using System.Reflection;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Data;

public class WeatherResponse
{
    public string status { get; set; }
    public string count { get; set; }
    public string info { get; set; }
    public string infocode { get; set; }
    public List<Forecast> forecasts { get; set; }
}

public class Forecast
{
    public string city { get; set; }
    public string adcode { get; set; }
    public string province { get; set; }
    public string reporttime { get; set; }
    public List<Cast> casts { get; set; }
}

public class Cast
{
    public string date { get; set; }
    public string week { get; set; }
    public string dayweather { get; set; }
    public string nightweather { get; set; }
    public string daytemp { get; set; }
    public string nighttemp { get; set; }
    public string daywind { get; set; }
    public string nightwind { get; set; }
    public string daypower { get; set; }
    public string nightpower { get; set; }
}

public class WeatherForecastService
{
    public async Task<WeatherResponse> GetWeather(string cityCode = "110000")
    {
        try
        {
            var url = $"https://restapi.amap.com/v3/weather/weatherInfo?key=315f705f52615604772b35432034b150&city={cityCode}&extensions=all";
            var client = new HttpClient();
            var res = await client.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<WeatherResponse>(json);
            return response;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private Root JsonWeather;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost, HttpGet]
        public IActionResult Index(string location)
        {
            if (location == null)
            {
                location = "London";
            }
            using (WebClient web = new WebClient())
            {
                string s = web.DownloadString($"https://api.openweathermap.org/data/2.5/weather?q={location}&units=metric&appid=5672206b5ff40a5b87886276db8fe0f0");
                JsonWeather = JsonConvert.DeserializeObject<Root>(s);
            }
            long sunrise = JsonWeather.Sys.Sunrise + JsonWeather.Timezone;
            long sunset = JsonWeather.Sys.Sunset + JsonWeather.Timezone;
            int humadity = JsonWeather.Main.Humidity;
            WeatherModel weather = new WeatherModel
            {
                lon = JsonWeather.Coord.Lon,
                humidity = JsonWeather.Main.Humidity,
                pressure = JsonWeather.Main.Pressure,
                icon = JsonWeather.Weather[0].Icon,
                Now = DateTime.Now,
                visibility = JsonWeather.Visibility,
                lat = JsonWeather.Coord.Lat,
                description = JsonWeather.Weather[0].Description,
                temp =Convert.ToInt32( JsonWeather.Main.Temp),
                feels_like = Convert.ToInt32(JsonWeather.Main.FeelsLike),
                temp_max = Convert.ToInt32(JsonWeather.Main.TempMax),
                temp_min = Convert.ToInt32(JsonWeather.Main.TempMin),
                WindDeg = JsonWeather.Wind.Deg,
                WindSpeed = JsonWeather.Wind.Speed,
                name = JsonWeather.Name,
                Clouds = JsonWeather.Clouds.All,
                sunrise = DateTimeOffset.FromUnixTimeSeconds(sunrise).Hour.ToString() + ":" + DateTimeOffset.FromUnixTimeSeconds(sunrise).Minute.ToString(),
                sunset = DateTimeOffset.FromUnixTimeSeconds(sunset).Hour.ToString() + ":" + DateTimeOffset.FromUnixTimeSeconds(sunset).Minute.ToString(),
            };
            return View(weather);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

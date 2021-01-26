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
            WeatherModel weather = new WeatherModel
            {
                lon = JsonWeather.Coord.Lon,
                icon = JsonWeather.Weather[0].Icon,
                Now = DateTime.Now,
                lat = JsonWeather.Coord.Lat,
                description = JsonWeather.Weather[0].Description,
                temp = JsonWeather.Main.Temp,
                feels_like = JsonWeather.Main.FeelsLike,
                temp_max = JsonWeather.Main.TempMax,
                temp_min = JsonWeather.Main.TempMin,
                WindDeg = JsonWeather.Wind.Deg,
                WindSpeed = JsonWeather.Wind.Speed,
                name = JsonWeather.Name,
                Clouds = JsonWeather.Clouds.All,
                sunrise = UnixTimeStampToDateTime(JsonWeather.Sys.Sunrise),
                sunset = UnixTimeStampToDateTime(JsonWeather.Sys.Sunset),
            };
            return View(weather);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}

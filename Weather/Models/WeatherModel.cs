using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weather.Models
{
    public class WeatherModel
    {
        public double lon { get; set; }
        public DateTime Now { get; set; }
        public double lat { get; set; }
        public string description { get; set; }
        public double visibility { get; set; }
        public string icon { get; set; }
        public int temp { get; set; }
        public int feels_like { get; set; }
        public int temp_min { get; set; }
        public int temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double WindSpeed { get; set; }
        public int WindDeg { get; set; }
        public int Clouds { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string name { get; set; }
    }
}

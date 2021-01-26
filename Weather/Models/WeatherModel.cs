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
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
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

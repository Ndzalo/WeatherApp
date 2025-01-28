using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppp.Models
{
    public class WeatherModel
    {
        public class Weather
        {
            public string Main { get; set; }
            public string Description { get; set; }
            public string Icon { get; set; }
        }

        public class Main
        {
            public double Temp { get; set; }
            public double Feels_like { get; set; }
            public double Temp_min { get; set; }
            public double Temp_max { get; set; }
            public int Humidity { get; set; }
        }

        public class Coord
        {
            public double Lat { get; set; }
            public double Lon { get; set; }
        }

        public class WeatherResponse
        {
            public List<Weather> Weather { get; set; }
            public Main Main { get; set; }
            public string Name { get; set; }
            public Coord Coord { get; set; }
        }
    }
}

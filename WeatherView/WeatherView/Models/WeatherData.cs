using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WeatherView.Models
{
    public class WeatherData
    {
        public double Temp { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public string SourceName { get; set; }
        public string City { get; set; }

        public WeatherData()
        {
            Temp = 0;
            Humidity = 0;
            Pressure = 0;
            SourceName = "_";
            City = "_";
        }

      
    }
}
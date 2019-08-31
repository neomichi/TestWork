using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using WeatherView.Models;

namespace WeatherView.Code
{
    public static class ApiHelper
    {
        public static Task<string> GetPage(string url)
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync();
            }
        }

        public static WeatherData GetWeatherData(string source, string text)
        {
            var wd = new WeatherData();

            if (!String.IsNullOrEmpty(text))
            {
                var temp = Regex.Match(text, "temp\":[\"]?([0-9\\.]+)[\"]?,",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);
                var humidity = Regex.Match(text, "humidity\":[\"]?([0-9\\.]+)[\"]?,",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);
                var pressure = Regex.Match(text, "pressure\":[\"]?([0-9\\.]+)[\"]?,",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);

                if (temp.Success && humidity.Success && pressure.Success)
                {

                    var tempReal = Double.Parse(temp.Groups[1].Value, CultureInfo.InvariantCulture);
                    var humidityReal = Double.Parse(humidity.Groups[1].Value, CultureInfo.InvariantCulture);
                    var pressureReal = Double.Parse(pressure.Groups[1].Value, CultureInfo.InvariantCulture);

                    wd = new WeatherData
                    {
                        Temp = tempReal,
                        Humidity = humidityReal,
                        Pressure = pressureReal,
                        SourceName = source,

                    };
                }
              
            }
            return wd;
        }


    }
}
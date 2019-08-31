using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using System.Web.Hosting;

namespace WeatherView.Code
{
    public static class Constant{
   
        public static string CityPath = HostingEnvironment.MapPath("/Content/static/city.json");
        public static string OpenweathermapSource = @"http://api.openweathermap.org/data/2.5/weather?appid=72dbf232736a28b3f92a3d55c7d9a21b&q=";

    
     

    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;
using WeatherView.Code;
using WeatherView.Models;

namespace WeatherView.Controllers
{
    public class TestController : BaseController
    {
        public JsonResult GetWeather1(string city)
        {

            var url = String.Format("{0}{1}", Constant.OpenweathermapSource, city);
            var res = ApiHelper.GetPage(url);
            var data = ApiHelper.GetWeatherData("OpenWeathermap", res.Result);
            data.Temp = Math.Round(data.Temp - 273.15,3);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWeather2(string city)
        {
            var url = String.Format(
                     " https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22{0}%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys",
                     city);

            var res = ApiHelper.GetPage(url);
            var data = ApiHelper.GetWeatherData("YahooWeather", res.Result);
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }





        public JsonResult GetCities()
        {
            var cities = new CityView()
            {
                Cities = CityHelper.GetCities(),
            };
            return Json(cities, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Add(string cityname)
        {

            CityHelper.AddCity(cityname);


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(string cityname)
        {

            return Json(CityHelper.DeleteCity(cityname), JsonRequestBehavior.AllowGet);
        }







    }
}

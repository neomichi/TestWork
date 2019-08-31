using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherView.Models;

namespace WeatherView.Code
{
    public class BaseController:Controller
    {
        public ICityHelper CityHelper = DependencyResolver.Current.GetService<ICityHelper>();
    }
}
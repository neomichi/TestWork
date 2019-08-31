using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherView.Models
{
    public class CityView
    {
        public IEnumerable<City> Cities { get; set; }
    }
}
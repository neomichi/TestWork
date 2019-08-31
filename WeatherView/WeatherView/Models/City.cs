using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherView.Models
{
    public class City
    {
        [Display(Name = "Имя города")]
        [Required]
        public string Name { get; set; }
        public IEnumerable<WeatherData> Weathers { get; set; }
    }
}
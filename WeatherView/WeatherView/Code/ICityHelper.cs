using System.Collections.Generic;
using WeatherView.Models;

namespace WeatherView.Code
{
    public interface ICityHelper
    {
        IEnumerable<City> GetCities();
        bool AddCity(string cityname);
        bool DeleteCity(string cityname);
    }
}
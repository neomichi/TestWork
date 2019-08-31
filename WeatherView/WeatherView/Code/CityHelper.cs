using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WeatherView.Models;

namespace WeatherView.Code
{


    public class CityHelper : ICityHelper
    {
        private readonly string _filePath = "";
        private List<City> _cities = new List<City>();

        public CityHelper(string path)
        {

            if (File.Exists(path))
            {
                _filePath = path;
            }
            else
            {
                _filePath = path;
                _cities.Add(new City{Name = "Москва"});
                Save();
                throw new Exception("файл не найден");
            }
        }



        public IEnumerable<City> GetCities()
        {

            using (var sr = new StreamReader(_filePath, Encoding.Default))
            {
                var row = sr.ReadToEnd();
                _cities = JsonConvert.DeserializeObject<List<City>>(row);
                sr.Close();
            }

            return _cities;
        }

        public bool AddCity(string cityname)
        {
            var state = _cities.Any(x => x.Name.Equals(cityname, StringComparison.OrdinalIgnoreCase));
            
            if (!state)
            {
                _cities.Add(new City {Name = cityname});
                Save();
            }
            return state;
        }

        public bool DeleteCity(string cityname)
        {
            var index = _cities.SingleOrDefault(x => x.Name.Equals(cityname, StringComparison.OrdinalIgnoreCase));

            if (index!=null)
            {
                _cities.Remove(index);
                Save();
            }
            return index != null;
        }

        private void Save()
        {
            var row = JsonConvert.SerializeObject(_cities);
            using (var sw = new StreamWriter(_filePath, false,Encoding.Default))
            {
                sw.Write(row);
                sw.Close();
            }
        }


    }
}
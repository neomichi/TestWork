using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models;
using FastFood.Web.Models.Enum;
using FastFood.Web.ModelView;

namespace FastFood.Web.ViewModels
{
    public class FastFoodView
    {       
        public string Name { get; set; }
        public FilterView Filter { get; set; }
        public CategoryType CategoryType { get; set; }      
    }   
       
}

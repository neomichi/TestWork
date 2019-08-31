using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;
using FastFood.Web.ViewModels;

namespace FastFood.Web.ModelView
{
    public class FinalView
    {
        public string CategoryTitle { get; set; }

        public CategoryType CategoryType { get; set; }

        public List<FastFoodView> foodList { get; set; }
    }
}

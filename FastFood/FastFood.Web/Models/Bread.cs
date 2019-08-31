using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Bread : BaseProduct
    {
        public override string Description => "хлеб";
        public override int Price => 10;
        public override  CategoryType ProductType => CategoryType.Food;
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Jam: BaseProduct
    {
        public override string Description => "джем";
        public override int Price => 10;
        public override  CategoryType ProductType => CategoryType.AdditionFood;
    }
}

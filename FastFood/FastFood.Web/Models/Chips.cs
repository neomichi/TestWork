using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Chips : BaseProduct
    {
        public override string Description => "чипсы";
        public override int Price => 34;
        public override CategoryType ProductType => CategoryType.Food;
    }      
}

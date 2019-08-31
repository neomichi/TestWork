using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Water : BaseProduct
    {
        public override string Description => "вода";
        public override int Price => 5;
        public override CategoryType ProductType => CategoryType.Drink;
    }


}

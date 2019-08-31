using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Cookies : BaseProduct
    {
        public override string Description => "печенье";
        public override int Price => 29;
        public override CategoryType ProductType => CategoryType.Food;
    }
}

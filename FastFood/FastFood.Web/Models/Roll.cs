using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Roll: BaseProduct
    {
        public override string Description => "булочка";
        public override int Price => 15;
        public override CategoryType ProductType => CategoryType.Food;
    }
}

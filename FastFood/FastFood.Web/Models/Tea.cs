using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Tea : BaseProduct
    {
        public override string Description => "черный чай";
        public override int Price => 25;
        public override CategoryType ProductType => CategoryType.Drink;
    }
}

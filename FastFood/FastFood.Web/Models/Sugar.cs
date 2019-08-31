using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Sugar : BaseProduct
    {
        public override string Description => "сахар";
        public override int Price => 5;
        public override CategoryType ProductType => CategoryType.AdditionDrink;

        public override int MaxCount => 5;

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public class Cheese : BaseProduct
    {
        public override string Description => "cыр";
        public override int Price => 10;
        public override CategoryType ProductType => CategoryType.AdditionFood;

      
    }
}

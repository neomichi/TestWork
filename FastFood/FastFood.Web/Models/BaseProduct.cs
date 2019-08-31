using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastFood.Web.Models.Enum;

namespace FastFood.Web.Models
{
    public abstract class BaseProduct : IBaseProduct
    {
        public abstract int Price { get; }
        public abstract string Description { get; }
        public abstract CategoryType ProductType { get; }
        public virtual int MaxCount { get; } = 1;

    }
}

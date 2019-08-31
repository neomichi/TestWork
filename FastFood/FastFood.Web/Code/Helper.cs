using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FastFood.Web.Models;
using FastFood.Web.Models.Enum;
using FastFood.Web.ModelView;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FastFood.Web.Code
{
    public static class Helper
    {

        public static string Description(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes =
               (DescriptionAttribute[])
             fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }



        public static FilterView GetSumm(IBaseProduct[] Products)
        {
            return new FilterView()
            {
                SumPrice = Products.Sum(x => x.Price),
                MaxCount = Products.Min(x => x.MaxCount)
            };     

            
        }

    }
}

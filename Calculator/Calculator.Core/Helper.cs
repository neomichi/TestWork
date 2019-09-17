using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Core
{
    public static class Helper
    {
        /// <summary>
        /// remove last char or default
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static string RemoveLast(this string row)
        {
            return row.Length > 0 ? row.Substring(0, row.Length - 1) : "";
        }

       
        public static string IfNegativeStr(this string row)
        {
            return row.First().Equals('_') ? row.Substring(1) : row.Insert(0,"_");
        }

        public static string ToClearString(this decimal value)
        {
            return value.ToString("0.######");
        }


        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value.Replace(",", "."), CultureInfo.InvariantCulture);
        }


    }
}

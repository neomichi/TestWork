using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Core;

namespace Calculator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PostfixNotationExpression a = new PostfixNotationExpression();

            //var ss = a.result("_2^2");
            Console.WriteLine(Decimal.Parse("0.5", CultureInfo.InvariantCulture));
            Console.WriteLine(Decimal.Parse("0,5", CultureInfo.InvariantCulture));

            Console.ReadKey();
        }

     
    }
}

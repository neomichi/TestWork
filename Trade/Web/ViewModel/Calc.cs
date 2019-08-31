using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model;

namespace Web.ViewModel
{
    public class Calc
    {
        public Order Pk { get; set; }
        public Order Pp { get; set; }

        public double Summa { get; set; }
        public int AmountMin { get; set; }

    }
}

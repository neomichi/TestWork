using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Data.ViewModels
{
    public class BuyTiketModelView
    {
        public Guid SeancesId { get; set; }     
        public int QuantityTickets { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.WFP
{
    public class SeansesView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }    
        public int FreePlaces { get; set; } = 0;
    } 
}
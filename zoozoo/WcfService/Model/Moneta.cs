using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Model
{

    public class Moneta
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Text { get; set; }
        public string Img { get; set; }
        public string Contact { get; set; }
        public DateTime CreateIt { get; set; }
        public int CountViews { get; set; }

    }
}
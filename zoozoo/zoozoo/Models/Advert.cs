using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zoozoo.Models
{
    public class Category
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string IconPath { get; set; }
    }

    public class Message
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateIn { get; set; }
        public string Region { get; set; }
        public string Author { get; set; }
        public int? Price { get; set; }
        public  IEnumerable<string> Photo { get; set; }



    }

  


    
}
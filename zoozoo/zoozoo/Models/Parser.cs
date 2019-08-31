using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using zoozoo.Code;

namespace zoozoo.Models
{

    public class ParserStateView
    {
        public IEnumerable<string> Info { get; set; }
        public bool IsWork { get; set; }
            
    }


   
}
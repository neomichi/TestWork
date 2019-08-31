using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motiv.Data
{
    public class SyncData : ISyncData
    {
        public int Progress { get; set; }
        public string Message { get; set; }
    }
}

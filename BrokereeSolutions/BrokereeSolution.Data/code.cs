using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BrokereeSolution.Data
{
    public static class code
    {
        public static ConcurrentDictionary<int, string> items = new ConcurrentDictionary<int, string>();
    }
}

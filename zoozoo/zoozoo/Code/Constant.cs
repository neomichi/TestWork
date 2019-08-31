using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Web;

namespace zoozoo.Code
{
    public static class Constant
    {
     

        public static string GetCountPageUrl = @"http://localhost:5995/ParserService.svc/GetCount";

        public static string GetPageUrl(int num)
        {
            return String.Format(@"http://localhost:5995/ParserService.svc/getpage/{0}", num);

        }

    }
}
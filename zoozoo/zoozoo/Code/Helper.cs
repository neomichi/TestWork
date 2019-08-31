using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace zoozoo.Code
{
    public static class Helper
    {
        public static string GetHtmlPage(string url)
        {
            var text = string.Empty;

            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.Default;
                try
                {
                    text = wc.DownloadString(url);
                }
                catch
                {
                    throw new Exception("webclient error");
                }
            }
            return text;
        }

        public static MvcHtmlString GetClass(this HtmlHelper html, string action, string controller)
        {
            if (
                action.Equals(html.ViewContext.RouteData.GetRequiredString("action"),
                StringComparison.OrdinalIgnoreCase) &&
                controller.Equals(html.ViewContext.RouteData.GetRequiredString("controller"),
                    StringComparison.OrdinalIgnoreCase))
            {
                return new MvcHtmlString("active");
            }
            return new MvcHtmlString("");


        }


        public sealed class Parser
        {
            private static volatile Parser _instance;
            private static readonly object SyncRoot = new Object();

            public Queue<string> Queue;
            public bool IsWork = false;
         
         
            private Parser()
            {
                IsWork = false;
                Queue = new Queue<string>();
            }

            public void Start()
            {
                Queue = new Queue<string>();
                IsWork = true;
                const string query = @"http://localhost:5995/ParserService.svc/GetCount";
                var response = GetHtmlPage(query);
                var n = int.Parse(response);
                var k = n/20;
                if (n%20 > 1) k++;

                Queue.Enqueue(String.Format("получаем кол-во обьявлений: {0} ", response));
                Queue.Enqueue(String.Format("число запросов: {0} ", k));

                var t = Task.Factory.StartNew(() =>
                {
                    Task i1 = Task.Factory.StartNew(() => Dowork(0));
                    Task i2 = Task.Factory.StartNew(() => Dowork(1));

                    Task.WaitAll(i1, i2);

                }).ContinueWith(x => Final("end"));


            }

            string Dowork(int num)
            {
                Queue.Enqueue(String.Format("получаем страницу №{0}", num));
                var res=GetHtmlPage(Constant.GetPageUrl(num));
                Queue.Enqueue(String.Format("страница  №{0} получена", num));
                return res;
            }

            void Final(string data)
            {
                Queue.Enqueue("записываем результат в дб ");
                Queue.Enqueue(String.Format("парсер {0}", data));
                IsWork = false;

            }


            public static Parser Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        lock (SyncRoot)
                        {
                            if (_instance == null)
                                _instance = new Parser();
                        }
                    }
                    return _instance;
                }
            }
        }

       
    }
}
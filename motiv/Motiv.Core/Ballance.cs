using HtmlAgilityPack;
using Motiv.Core.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


namespace Motiv.Core
{
    public static class Ballance
    {
        private static Subscription subscriptions { get; set; }
        private static CookieContainer cookieContainer { get; set; }

        static string WebQuery(string url, CookieContainer cc, string postData = "", bool AddCookie = false)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = 5000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36 OPR/46.0.2597.39";
            request.CookieContainer = cc;
            if (!string.IsNullOrEmpty(postData))
            {
                var data = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = data.Length;
                request.Method = "POST";
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
       
            var response = request.GetResponse();
            if (response!=null)
            if (AddCookie)
            {
                cookieContainer = new CookieContainer();
                cookieContainer.Add(((HttpWebResponse) response).Cookies);

            }
            using (var stream = ((HttpWebResponse)response).GetResponseStream())
            {
                using (var streamReader = new StreamReader(stream, true))
                {
                    return streamReader.ReadToEnd();
                }
            }

        }


     
        static bool FindText(HtmlNode node, string row)
        {
            return node.InnerText.IndexOf(row, StringComparison.OrdinalIgnoreCase) > -1;
        }

        /// <summary>
        /// GetSubscriptionFromHtml
        /// </summary>
        /// <param name="text">html</param>
        /// <returns></returns>
        static Subscription GetSubscriptionFromHtml(string text)
        {
            //var txt = @"C:\Users\neomichi\Desktop\1.html";
            //var test = System.IO.File.ReadAllText(txt);

            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            var table = doc.DocumentNode.Descendants("table")
                .Where(x =>
                FindText(x, "Интернет")
                && FindText(x, "Остаток")
                && FindText(x, "Период действия")).FirstOrDefault();

            if (table == null)
            {
                throw new Exception("not found");
            }

            var sub = new Subscription();
            var data = table.Descendants("td")
            .Where(x => FindText(x, "от")).FirstOrDefault();

            var list = new List<DateTime>();

            foreach (Match date in Regex.Matches(data.InnerText, Constant.RegexDateTime))
            {
                list.Add(DateTime.Parse(date.Groups[1].Value));
            }

            if (list[0] > list[1])
            {
                sub.Start = list[1];
                sub.End = list[0];
            }
            else
            {
                sub.Start = list[0];
                sub.End = list[1];
            }


            sub.FreeTraffics = Double.Parse(Regex.Match(table.InnerText, Constant.RegexDuoble).Groups[1].Value, CultureInfo.InvariantCulture);

            return sub;
        }


        static string GetHtmlFromCabinetPage(AuthData auth)
        {
           
            var url1 = "https://lisa.motivtelecom.ru/";
            var url2 = "https://lisa.motivtelecom.ru/rest_of_packets/";

            WebQuery(url1, cookieContainer,"",true);

            var postData = string.Format("logintype={0}&abnum={1}&pass={2}", auth.AuthType, auth.Login, auth.Password);

            WebQuery(url1, cookieContainer, postData);
            return WebQuery(url2, cookieContainer, postData); ;
        }


        public static Subscription GetBallanse(AuthData auth)
        {
            var text = Core.Ballance.GetHtmlFromCabinetPage(auth);
            var res = Core.Ballance.GetSubscriptionFromHtml(text);

            var period = (res.End - res.Start).TotalSeconds;
            var g = period;
            var available = (DateTime.Now - res.Start).TotalSeconds;
            res.Median = 20 - 20.0 * available / period;

            var leftSecond = (res.End - DateTime.Now);
            if (leftSecond.TotalMinutes < 60.00)
            {
                var array = Constant.GetDays((int)leftSecond.TotalMinutes).Split('|');
                res.SecondsLeft = string.Format("{0}{1}", array[1], array[4]);
            }
            else if (leftSecond.TotalDays > 1.0)
            {
                var array = Constant.GetDays((int)leftSecond.TotalDays).Split('|');
                res.SecondsLeft = string.Format("{0}{1}", array[1], array[2]);
            }
            else if (leftSecond.TotalHours < 24.00)
            {
                var array = Constant.GetDays((int)leftSecond.TotalHours).Split('|');
                res.SecondsLeft = string.Format("{0}{1}", array[1], array[3]);
            }

    
            return res;
        }
    }
}

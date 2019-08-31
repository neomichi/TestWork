using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using HtmlAgilityPack;
using Motiv.Data.Model;

namespace Motiv.Data
{
    public class Ballance : IBallance
    {
        CookieContainer cookieContainer;

        IAuthData authData;
        Subscription subscriptions { get; set; }

        /// <summary>
        /// делегат
        /// </summary>
        /// <param name="state">состояние</param>
        public delegate void ProgressHandler(int state = 0);
        /// <summary>
        ///  если изменилось
        /// </summary>
        public event ProgressHandler Change;
        public Ballance()
        {
            cookieContainer=new CookieContainer();
        }

  

       

    
        
       

        /// <summary>
        /// перевод в понятные еденицы
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static string ToStringCalcWithUnit(double val)
        {
            
            string trafficUnit = "";
            if (val > 1024 * 1024)
            {
                trafficUnit = "гб";
                val /= (1024 * 1024);
            }
            else if (val > 1024)
            {
                trafficUnit = "мб";
                val /= 1024;
            }
            else if (val > 0)
            {
                trafficUnit = "кб";
            }
           
            return string.Format("{0:0.0000} {1}", val, trafficUnit);
        }

        static string WebQuery(string url,CookieContainer cc, string postData = "", bool AddCookie = false)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
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
            if (response == null)
            {
                throw new WebException("Нет ответа");
            }
            else if (AddCookie)
            {                
                cc.Add(((HttpWebResponse)response).Cookies);
            }
            using (var stream = ((HttpWebResponse)response).GetResponseStream())
            {
                using (var streamReader = new StreamReader(stream, true))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        //ssl
        static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
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
        Subscription GetSubscriptionFromHtml(string rawHtml)
        {
           
            var optionsList = new List<Option>();
            var dateTimeList = new List<DateTime>();
            var subscription = new Subscription();
            double freeTraffic = 0;
            var doc = new HtmlDocument();
            doc.LoadHtml(rawHtml);
            var alltr = doc.DocumentNode.Descendants("tr");
            var table = doc.DocumentNode.Descendants("table")
                .Where(x =>
                FindText(x, "Интернет")
                && FindText(x, "Остаток")
                && FindText(x, "Срок действия")).FirstOrDefault();

            if (table == null)
            {
                throw new Exception("page not found");
            }


            var ballance = doc.DocumentNode.Descendants("tr")
                .FirstOrDefault(x => x.InnerText.Contains("Баланс:"));
               
            if (ballance!=null)
            {
                subscription.Ballance = Regex.Match(ballance.InnerText, Constant.RegexDouble).Value.Trim()+"р";              
                    
            }



            var listTr = table.Descendants("tr");

            var trTitle = alltr.FirstOrDefault(x => x.InnerText.IndexOf("Тарификация:") > -1);
            if (trTitle!=null)
            {
                subscription.Tariff = trTitle.Descendants("td")
                     .Select(x => x.InnerText.Trim()).ToList()[1];                
            }

            

            var listTrFilter = listTr.Where(x => x.InnerText.Contains(authData.Tarif)
            || x.InnerText.Contains(authData.Option)).ToList();

           
            foreach (var tr in listTrFilter)
            {

                var customDateTimeList = new List<DateTime>();

                var tdList = tr.Descendants("td").Select(x => string.IsNullOrEmpty(x.InnerText) ? "" :
                x.InnerText.Trim().Replace("\r\n", string.Empty)).ToList();

                foreach (Match date in Regex.Matches(tdList[2], Constant.RegexDateTime))
                {
                    customDateTimeList.Add(DateTime.Parse(date.Groups[1].Value));
                }
                var regexValue = Regex.Match(tdList[1], Constant.RegexDoubleCost);
                if (regexValue.Success && regexValue.Groups.Count > 2)
                {
                  
                    var row = regexValue.Groups[3].Value.Trim();

                   // var coaf = 1;


                    if (row.Equals("GB", StringComparison.OrdinalIgnoreCase))
                        freeTraffic += Double.Parse(regexValue.Groups[1].Value, CultureInfo.InvariantCulture) * 1024 * 1024;
                    if (row.Equals("MB", StringComparison.OrdinalIgnoreCase))
                        freeTraffic += Double.Parse(regexValue.Groups[1].Value, CultureInfo.InvariantCulture) * 1024;

                   // coaf *= 1024;
                    /*if (row.Equals("B", StringComparison.OrdinalIgnoreCase)) coaf *= 1;*/



                    dateTimeList.AddRange(customDateTimeList);
                }
                else
                {
                    var option = new Option()
                    {
                        End = customDateTimeList.Max(x => x),
                    };
                    subscription.Options = new List<Option> { option };
                }

            }
        

            subscription.Start = dateTimeList.Min(x => x);
            subscription.End = dateTimeList.Max(x => x);

            if (subscription.End.Value - subscription.Start.Value < TimeSpan.FromHours(1))
                subscription.Start = subscription.Start.Value.AddDays(-30);


            subscription.FreeTraffic = ToStringCalcWithUnit(freeTraffic);           
          

            var period = (subscription.End.Value - subscription.Start.Value).TotalSeconds;
            var available = (subscription.End.Value - DateTime.Now);
            subscription.Median = ToStringCalcWithUnit(freeTraffic * available.TotalSeconds / (period));

            subscription.TimeLeft = TimeLeft(available);


           
            return subscription;
        }

        


        string GetHtmlFromCabinetPage(int oneTime, int twoTime)
        {

           

            WebQuery(Constant.Url1, cookieContainer, "", true);
            Thread.Sleep(oneTime);
            Change.Invoke(20);
            var postData = string.Format("logintype={0}&abnum={1}&pass={2}", authData.AuthType, authData.Login, authData.Password);
        
            WebQuery(Constant.Url1, cookieContainer, postData);
            Change.Invoke(50);
            Thread.Sleep(twoTime);

            return WebQuery(Constant.Url2, cookieContainer, postData); ;
        }


        //start
        public Subscription GetBallanse(IAuthData auth,int oneTime,int twoTime ) 
        {

            Change.Invoke(1);
            authData = auth;
            Change.Invoke(5);
            var rawHtml = GetHtmlFromCabinetPage(oneTime, twoTime);
            Change.Invoke(70);
       
            var subscription = GetSubscriptionFromHtml(rawHtml);
            Change.Invoke(90);
       
        
            if (subscription.Options != null)
            {
                subscription.Options[0].Title = TimeLeft(subscription.Options[0].End - DateTime.Now);
            }
            Change.Invoke(100);

            return subscription;
        }

        static string TimeLeft(TimeSpan time)
        {

            if (time.TotalMinutes < 60.00)
            {
                return string.Format("{0} {1}",(int) time.Minutes, Constant.GetStringTime(Constant.TimeRangeEnum.Min, time));
            }
            else if (time.TotalDays > 1.0)
            {
                return string.Format("{0} {1}", (int)time.TotalDays, Constant.GetStringTime(Constant.TimeRangeEnum.Day, time));

            }
            else if (time.TotalHours < 24.00)
            {
                return string.Format("{0} {1}", (int)time.TotalHours, Constant.GetStringTime(Constant.TimeRangeEnum.Hour, time));

            }
            return "";

        }

        /// <summary>
        /// проверка на наличие интернета
        /// </summary>
        /// <returns></returns>
        public static bool CheckPing()
        {
            try
            {
                Ping myPing = new Ping();
                PingReply reply = myPing.Send("ya.ru", 1000);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

    }
}

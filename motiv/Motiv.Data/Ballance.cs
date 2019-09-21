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
using EasyHttp.Http;
using HtmlAgilityPack;
using Motiv.Data.Model;

namespace Motiv.Data
{
    public class Ballance : IBallance
    {
        CookieContainer cookieContainer;
        CookieCollection cookieCollection;
        IAuthData authData;
        Subscription subscriptions { get; set; }
        HttpClient httpClient;
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

            cookieCollection = new CookieCollection();

            httpClient = new HttpClient();
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

        string HttpClient(string url,string postData = "")
        { 
            HttpResponse httpResponse;
            
            if (string.IsNullOrWhiteSpace(postData))
            {
                httpResponse = httpClient.Get(url);
                cookieCollection = httpResponse.Cookies;
                return httpResponse.RawText;

            } else
            {
                httpClient.Request.Cookies = cookieCollection;
                httpClient.Request.Host = "lisa.motivtelecom.ru";
                httpResponse = httpClient.Post(url, postData, "application/x-www-form-urlencoded");
            }
            return httpResponse.RawText;
        }
        //[Obsolete]
        //static string WebQuery(string url,CookieContainer cc, string postData = "", bool AddCookie = false)
        //{
        //    var request = (HttpWebRequest)HttpWebRequest.Create(url);
        //    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        //    request.Timeout = 5000;
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36 OPR/46.0.2597.39";
        //    request.CookieContainer = cc;
        //    if (!string.IsNullOrEmpty(postData))
        //    {
        //        var data = Encoding.UTF8.GetBytes(postData);
        //        request.ContentLength = data.Length;
        //        request.Method = "POST";
        //        using (var stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }
        //    }

        //    var response = request.GetResponse();
        //    if (response == null)
        //    {
        //        throw new WebException("Нет ответа");
        //    }
        //    else if (AddCookie)
        //    {                
        //        cc.Add(((HttpWebResponse)response).Cookies);
        //    }
        //    using (var stream = ((HttpWebResponse)response).GetResponseStream())
        //    {
        //        using (var streamReader = new StreamReader(stream, true))
        //        {
        //            return streamReader.ReadToEnd();
        //        }
        //    }
        //}

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
            var doc = new HtmlDocument();
            doc.LoadHtml(rawHtml);
            var alltr = doc.DocumentNode.Descendants("tr");
           var divUserInfo = doc.DocumentNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("id") &&
            x.Attributes["id"].Value.Equals("userinfo"));

            var divAll = doc.DocumentNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("id") &&
        x.Attributes["id"].Value.Equals("all"));


            if (divUserInfo == null || divAll == null)
            {
                throw new Exception("page not found");
            }


            //var trD = divAll.Descendants("tr").ToList();
            //var tddList = trD.SelectMany(x => x.Descendants("td")
            //.Select(y => y.InnerText.Trim()).ToList()).ToList();

            //var output = Enumerable.Range(0, tddList.Count / 2)
            //           .Select(i => Tuple.Create(tddList[i * 2], tddList[i * 2 + 1]))
            //           .ToList();


            var aditionProps = Helper.GetParams(divAll);
            var mainProps= Helper.GetParams(divUserInfo);







            //var table = doc.DocumentNode.Descendants("table")
            //    .Where(x =>
            //    FindText(x, "Интернет")
            //    && FindText(x, "Остаток")
            //    && FindText(x, "Срок действия")).FirstOrDefault();

            //if (table == null)
            //{
            //    throw new Exception("page not found");
            //}
          
            subscription.Ballance = mainProps.First(x => x.Item1
           .IndexOf("Баланс", StringComparison.OrdinalIgnoreCase) > -1).Item2;

            subscription.Tariff= mainProps.First(x => x.Item1
             .IndexOf("Тарификация", StringComparison.OrdinalIgnoreCase) > -1).Item2;






            subscription.End = DateTime.MinValue;




            decimal traffic = 0;

            foreach (var tr in aditionProps)
            {
                var row = tr.Item1 + tr.Item2;
                var regexValue = Regex.Match(row, Helper.RegexDoubleCost);
                var regexDate = Regex.Match(row, Helper.RegexDt);

                if (regexValue.Success && regexValue.Groups.Count > 2)
                {
                    var unit = regexValue.Groups[3].Value.Trim().ToUpper();
                    var val= regexValue.Groups[1].Value.Trim();                   
                    traffic += Helper.GetUnitValueToByte(unit, val);
                }

                if (regexDate.Success)
                {
                    var date = DateTime.Parse(regexDate.Value);
                    if (subscription.End < date)
                    {
                        subscription.End = date;
                        subscription.Start=date.AddDays(-30);
                    }

                }
            }
     
           
            subscription.FreeTraffic = ToStringCalcWithUnit(Decimal.ToDouble(traffic));

            var period = (subscription.End.Value - subscription.Start.Value).TotalSeconds;
            var available = (subscription.End.Value - DateTime.Now);
            subscription.Median = ToStringCalcWithUnit(Decimal.ToDouble(traffic) * available.TotalSeconds / (period));

            subscription.TimeLeft = TimeLeft(available);


           
            return subscription;
        }

        


        string GetHtmlFromCabinetPage(int oneTime, int twoTime)
        {



            HttpClient(Helper.Url1,"");
            Thread.Sleep(oneTime);
            Change.Invoke(20);
            var postData = string.Format("logintype={0}&abnum={1}&pass={2}", authData.AuthType, authData.Login, authData.Password);
            Change.Invoke(50);
            Thread.Sleep(twoTime);
            HttpClient(Helper.Url1, postData);
            Change.Invoke(60);
            var a= HttpClient(Helper.Url2);

            return a;
        }


        //start
        public Subscription GetBallanse(IAuthData auth,int oneTime,int twoTime ) 
        {

            Change.Invoke(1);
            authData = auth;
            Change.Invoke(5);

            
            var rawHtml = File.ReadAllText(@"F:\svn\motiv.helper\Motiv.Wpf.Ballance\gg.txt");
           // var rawHtml =GetHtmlFromCabinetPage(oneTime, twoTime);
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
                return string.Format("{0} {1}",(int) time.Minutes, Helper.GetStringTime(Helper.TimeRangeEnum.Min, time));
            }
            else if (time.TotalDays > 1.0)
            {
                return string.Format("{0} {1}", (int)time.TotalDays, Helper.GetStringTime(Helper.TimeRangeEnum.Day, time));

            }
            else if (time.TotalHours < 24.00)
            {
                return string.Format("{0} {1}", (int)time.TotalHours, Helper.GetStringTime(Helper.TimeRangeEnum.Hour, time));

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

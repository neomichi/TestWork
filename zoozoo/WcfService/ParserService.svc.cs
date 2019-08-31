using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WcfService.Code;
using WcfService.Model;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ParserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ParserService.svc or ParserService.svc.cs at the Solution Explorer and start debugging.
    public class ParserService : IParserService
    {


        public IEnumerable<string> GetData(string value)
        {
            return new List<string> { "1", "2" };
        }

        public int GetCount()
        {
            var html = Helper.GetHtmlAsync(Constant.UrlCountPages);
            var div =
                html.Result.DocumentNode.Descendants("div")
                    .Where(x => x.Attributes.Contains("align") && x.Attributes["align"].Value.Contains("left"));
            return (from el in div
                    select
                        Regex.Match(el.InnerText, Constant.RegexGetCount, RegexOptions.IgnoreCase | RegexOptions.Compiled)
                        into c
                        where c.Success
                        select int.Parse(c.Groups[1].Value)).FirstOrDefault();

        }

        public IEnumerable<Moneta> GetPage(string page)
        {
            var qq = new Queue<Moneta>();
            var pagenum = 0;
            int.TryParse(page, out pagenum);
            var html = Helper.GetHtmlAsync(Constant.GetParentUrl(pagenum));
            var table = Helper.GetSingleTag(html.Result, "table", "class", "adsmanager_table");
              

            if (table != null)
                foreach (var tr in table.Descendants("tr"))
                {

                    var tdAdsmanager = Helper.GetSingleNodeTag(tr, "td", "class", "adsmanager_table_description");
                    var tdCenter = Helper.GetSingleNodeTag(tr, "td", "class", "center");

                    if (tdAdsmanager != null && tdCenter != null)
                    {
                        var regexCreateIn = Regex.Match(tdCenter.InnerHtml, Constant.RegexCreateDate,
                            RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        var regexCount = Regex.Match(tdCenter.InnerHtml, Constant.RegexViewsCount,
                            RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        var regexId = Regex.Match(tdAdsmanager.InnerHtml, Constant.RegexGetItemId,
                            RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        var regexCatId = Regex.Match(tdAdsmanager.InnerHtml, Constant.RegexGetItemCatId,
                            RegexOptions.IgnoreCase | RegexOptions.Compiled);

                        if (!regexId.Success || !regexCatId.Success || !regexCreateIn.Success || !regexCount.Success)
                            continue;

                        var id = int.Parse(regexId.Groups[1].Value);
                        var catId = int.Parse(regexCatId.Groups[1].Value);
                        var date = DateTime.ParseExact(regexCreateIn.Value, "dd.MM.yyyy",CultureInfo.InvariantCulture);
                        var countViews = Helper.IntParce(regexCount.Groups[1].Value);
                        qq.Enqueue(GetItem(catId, id, date, countViews));
                    }
                }

            return qq.ToList();

        }

        static Moneta GetItem(int categoryid, int id, DateTime dt, int countViews)
        {
            var html = Helper.GetHtmlAsync(Constant.GetItemUrl(categoryid, id)).Result;
            var title = Helper.GetSingleTag(html, "h2", "class", "adsmanager_ads_title");
            var price = Helper.GetSingleTag(html, "div", "class", "adsmanager_ads_price");
            var text = Helper.GetSingleTag(html, "div", "class", "adsmanager_ads_desc");
            var contactText = Helper.GetSingleTag(html, "div", "class", "adsmanager_ads_contact");
            var img = Helper.GetSingleTag(html, "div", "class", "adsmanager_ads_image");

       

            var contact = new StringBuilder();
            if (contactText != null)
                foreach (var childText in contactText.ChildNodes)
                {
                    var str = childText.InnerText.Trim();
                    if (!String.IsNullOrWhiteSpace(str))
                    {
                        contact.AppendLine(str);
                    }

                }


            var moneta = new Moneta
            {
                Id = id,
                CategoryId = categoryid,
                Price = 0,
                Text = text != null ? text.InnerText.Trim() : String.Empty,
                Title = title != null ? title.InnerText.Trim() : String.Empty,
                Contact = contact.ToString(),
                CountViews = countViews,
                CreateIt = dt,
                Img = string.Empty
            };

            if (img != null)
            {
                var src = img.Descendants("img").SingleOrDefault(x => x.Attributes.Contains("src"));
                if (src != null)
                {
                    moneta.Img = src.Attributes["src"].Value;
                }


            }




            if (price == null) return moneta;

            var pricetext = Regex.Match(price.InnerText, Constant.RegexGetInt,
                RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (pricetext.Success)
                moneta.Price = Helper.IntParce(pricetext.Value);





            return moneta;

        }


    }
}

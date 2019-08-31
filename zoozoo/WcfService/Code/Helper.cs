using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace WcfService.Code
{
    public static class Helper
    {

        public static async Task<HtmlDocument> GetHtmlAsync(string url)
        {
            using (var http=new HttpClient())
            {
                var response= await http.GetByteArrayAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(Encoding.Default.GetString(response));
                return doc;
            }

            
        }

        public static int IntParce(string row)
        {
             int def = 0;
            int.TryParse(row, out def);
            return def;
        }
        /// <summary>
        ///  получить 1 тег с с атрибутом и значением аттрибута
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tag"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static HtmlNode GetSingleTag(HtmlDocument html,string tag,string attribute,string value)
        {
            return GetSingleNodeTag(html.DocumentNode, tag, attribute, value);
        }

        public static HtmlNode GetSingleNodeTag(HtmlNode html, string tag, string attribute, string value)
        {
            var res = html.Descendants(tag)
                    .SingleOrDefault(
                        x =>
                            x.Attributes.Contains(attribute) &&
                            x.Attributes[attribute].Value.Contains(value));
            return res;
        }

     
    }
}
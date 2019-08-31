using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService.Code
{
    public static class Constant
    {

        public const int PageLimit = 20;


        public static string UrlCountPages = @"http://monetki.su/index.php?option=com_adsmanager&page=show_all&Itemid=39";

        /// <summary>
        ///  получить страницу с обьявлениями
        /// </summary>
        /// <param name="numpage">с какой страницы</param>
        /// <returns></returns>
        public static string GetParentUrl(int numpage)
        {
            return String.Format("http://monetki.su/component/option,com_adsmanager/page,show_all/text_search,/order,0/expand,0/Itemid,39/limit,{0}/limitstart,{1}/", PageLimit, numpage * PageLimit);

        }

        /// <summary>
        /// получить конкретную страницу с обьявлением
        /// </summary>
        /// <param name="categoryid">категория</param>
        /// <param name="id">ид</param>
        /// <returns></returns>
        public static string GetItemUrl(int categoryid,int id)
        {
            return String.Format("http://monetki.su/component/option,com_adsmanager/page,show_ad/adid,{0}/catid,{1}/Itemid,/", id, categoryid);

        }

        public static string RegexGetCount = @"из[ ]+(\d+)";

        public static string RegexGetItemCatId = @"catid,(\d+)";
        public static string RegexGetItemId = @"adid,(\d+)";

        public static string RegexCreateDate = @"\d{2}.\d{2}.\d{4}";
        public static string RegexViewsCount = @"Просмотров[ ]+=[ ]+(\d+)";
      
        public static string RegexGetInt = @"\d+";
    }
}
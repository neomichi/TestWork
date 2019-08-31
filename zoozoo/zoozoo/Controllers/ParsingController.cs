using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using zoozoo.Code;
using zoozoo.Models;

namespace zoozoo.Controllers
{
    public class ParsingController : Controller
    {
        //
        // GET: /Parsing/

        public ActionResult Index()
        {
            var p = new ParserStateView
            {
                Info = Helper.Parser.Instance.Queue.ToList(),
                IsWork = Helper.Parser.Instance.IsWork
            };

            return View(p);
        }

        public JsonResult Start()
        {
            if (!Helper.Parser.Instance.IsWork)
            {
                Helper.Parser.Instance.Start();
            }
            return Json(new ParserStateView
            {
                Info = Helper.Parser.Instance.Queue.ToList(),
                IsWork = Helper.Parser.Instance.IsWork
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Info()
        {
            return
                Json(
                    Helper.Parser.Instance.Queue.Count > 0
                        ? Helper.Parser.Instance.Queue.ToList()
                        : new List<string> { "не запущен" }, JsonRequestBehavior.AllowGet);
        }







    }
}

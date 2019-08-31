using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.Data.Services;

namespace Cinema.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private ISeanseService _seanseService;
        public HomeController(ISeanseService seanseService)
        {
            _seanseService = seanseService;
        }
        public ActionResult Index()
        {
           
            return View();
        }

       



    }
}
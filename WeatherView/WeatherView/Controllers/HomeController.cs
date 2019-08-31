using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WeatherView.Code;
using WeatherView.Models;

namespace WeatherView.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(5);
        }

 

        


    }
}

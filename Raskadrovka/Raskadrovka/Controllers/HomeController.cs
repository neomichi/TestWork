using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Raskadrovka.Code;
using Raskadrovka.Models;

namespace Raskadrovka.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var imgList=Helper.GetImgList();
            return View(imgList);
        }

        /// <summary>
        /// получить раскадровку по горизонтали
        /// </summary>
        /// <param name="width">ширина</param>
        /// <returns></returns>
        public JsonResult GetImgList(string width)
        {
            var w = JsonConvert.DeserializeObject<int>(width);
            var imgList = Helper.GetImgList().ToArray();
            
         
            var sol = Helper.GausJordan(imgList,w);

            var newimgList = new ImgFile[imgList.Length];
            for (var i = 0; i < imgList.Length; i++)
            {
                var newImgFile = new ImgFile
                {
                    H = (int) (sol[i]*imgList[i].H),
                    W = (int) (sol[i]*imgList[i].W),
                    Name=imgList[i].Name
                };

                newimgList[i] = newImgFile;
            }

            return Json(newimgList, JsonRequestBehavior.AllowGet);

        }


    }
}

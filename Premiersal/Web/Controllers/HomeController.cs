using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db;
        public HomeController()
        {
            db = DependencyResolver.Current.GetService(typeof(MyDbContext)) as MyDbContext;
        }

        public ActionResult Index()
        {
            var films = new Queue<Film>();
            foreach (var film in db.Films.ToList())
            {
                var purchases = db.Purchases.Where(x => x.Film == film.Id).ToList();
                film.FreeNumPlaces =film.NumPlaces-(purchases.Any() ?  purchases.Sum(x=>x.Tikets):0);

                films.Enqueue(film);
            }

            return View(films.ToList());
        }
        [HttpGet]
        public ActionResult AddOrEditFilm(int? filmid)
        {
            var film = new Film();
            if (filmid.HasValue)
            {

                film = db.Films.SingleOrDefault(x => x.Id == filmid.Value);
            }
            return View(film);
        }

        [HttpPost]
        public ActionResult AddOrEditFilm(Film film)
        {

            if (ModelState.IsValid)
            {
                //if (film.OrderStart >= film.OrderEnd)
                //{
                //    ModelState.AddModelError("","дата начала продажи не может быть больше даты окончания");
                //} 
                db.Films.AddOrUpdate(film);
              
          
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            TempData["error"] = ModelState;
            return RedirectToAction("AddOrEditFilm");

        }


        public ActionResult Delete(int id)
        {
            var film = db.Films.SingleOrDefault(x => x.Id == id);
            if (film != null)
            {
                db.Films.Remove(film);
                db.SaveChanges();
            }
            else
            {
                return HttpNotFound("не найден");
            }
            return RedirectToAction("Index");
        }
    }
}
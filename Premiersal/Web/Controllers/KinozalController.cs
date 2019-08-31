using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Web.Code;
using Web.Models;

namespace Web.Controllers
{
    public class KinozalController : ApiController
    {
        MyDbContext db;
        public KinozalController()
        {
            db = DependencyResolver.Current.GetService(typeof(MyDbContext)) as MyDbContext;
        }
        // GET api/<controller>
        public IEnumerable<Film> Get()
        {
            var films = new Queue<Film>();
            foreach (var film in db.Films.ToList())
            {
                var purchases = db.Purchases.Where(x => x.Film == film.Id).ToList();
                film.FreeNumPlaces = film.NumPlaces - (purchases.Any() ? purchases.Sum(x => x.Tikets) : 0);

                films.Enqueue(film);
            }

            return films;
        }

        // GET api/<controller>/5
        public IEnumerable<Film> Get(int id)
        {
            return db.Films.Where(x => x.Id == id).ToList();
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Purchase purchase)
        {
            //смотрим есть ли такой фильм
            var film = db.Films.SingleOrDefault(x => x.Id == purchase.Film);
            if (film != null){
                //если есть, считаем купленные места
                    //ef не умееет работать с нулевой суммой
                var purchases = db.Purchases.Where(x => x.Film == film.Id).ToList();
                var sum = purchases==null? 0 : purchases.Sum(x => x.Tikets);
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                       
                        if (sum + purchase.Tikets < film.NumPlaces)
                        {   purchase.Time=DateTime.Now;
                            db.Purchases.Add(purchase);
                            db.SaveChanges();
                            dbContextTransaction.Commit();
                            return Ok("success");
                        }
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        return BadRequest("не хватает мест");
                    }
                }
            }
            return BadRequest("не найдет такой фильм");
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
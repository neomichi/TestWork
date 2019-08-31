using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.Data.Services;
using Cinema.Data.ViewModels;
using Cinema.Web.Models;

namespace Cinema.Web.Controllers
{
   
    public class SpectatorsController : ApiController
    {
        private CinemaDbContext db;
        private ISeanseService _seanseService;

        public SpectatorsController(ISeanseService seanseService, CinemaDbContext _db)
        {
            _seanseService = seanseService;
            db = _db;
        }


        [HttpGet]
        [ResponseType(typeof(SpectatorsController))]
        public IHttpActionResult Spectators()
        {  
            return Json(_seanseService.GetActualSeances());
        }  

        [HttpPut]
        [ResponseType(typeof(SpectatorsController))]
        public IHttpActionResult Spectators(BuyTiketModelView tiket)
        {
            var seanse = db.CinemaSeances
                .FirstOrDefault(x => x.Start>DateTime.Now && x.Id == tiket.SeancesId);
            if (seanse == null)
            {
                return BadRequest("не найден сеанс");
            }

            var closedPlaces = db.SeanceSpectators.AsNoTracking()
                .Where(x => x.SeanceId == tiket.SeancesId).ToList()
                .Sum(x => x.QuantityTickets);            

            if (seanse.QuantityPlaces >= (closedPlaces + tiket.QuantityTickets))
            {
                db.SeanceSpectators.Add(new SeanceSpectator
                {
                    QuantityTickets = tiket.QuantityTickets,
                    SeanceId = tiket.SeancesId,
                    CreateIt = DateTime.Now,
                });
                db.SaveChanges();
                return Ok();
            }
            return BadRequest("недостаточно мест");
        }        
 
        public IHttpActionResult DeleteSpectator(Guid id)
        {   
            return Ok();
        }            
    }
}
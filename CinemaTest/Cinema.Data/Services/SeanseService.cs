using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Data.Models;
using Cinema.Data.ViewModels;

namespace Cinema.Data.Services
{
    public class SeanseService : ISeanseService
    {
        private CinemaDbContext db;
        public SeanseService(CinemaDbContext _db)
        {
            db = _db;
        }

        public int GetTest()
        {
            return db.Users.Count(); 
        }

        private Dictionary<Guid,int> GetDicSeanceSpectators(IQueryable<Seance> seances)
        {
           return seances.ToDictionary(x => x.Id, x => x.SeanceSpectators.Sum(y => y.QuantityTickets));
        }


        public List<SeansesView> GetActualSeances()
        {
            var seances = db.CinemaSeances
              .Where(x => x.Start > DateTime.Now)
              .Include(x => x.SeanceSpectators)
              .OrderBy(x => x.Start)
              .AsNoTracking();

            var dic = GetDicSeanceSpectators(seances);

            var seansesViews = seances.Select(x => new SeansesView
            {
                Title = x.Title,
                Id = x.Id,
                Start = x.Start,
                FreePlaces = x.QuantityPlaces
            }).ToList();

            foreach (var seansesView in seansesViews)
            {
                seansesView.FreePlaces = dic.Keys.Contains(seansesView.Id)
                    ? seansesView.FreePlaces - dic[seansesView.Id]
                    : seansesView.FreePlaces;
            }

            return seansesViews.Where(x=>x.FreePlaces>0).ToList();
        }

        public List<Seance> GetAllSeances()
        {
            var seances = db.CinemaSeances.Include(x=>x.SeanceSpectators).OrderBy(x => x.Start).AsNoTracking();
            var dic=GetDicSeanceSpectators(seances);
            var allSeances = seances.ToList();


           foreach (var seance in allSeances)
           {
                seance.OccupiedPlace = dic.Keys.Contains(seance.Id)
                   ? dic[seance.Id]
                   : 0;
            }
            return allSeances;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cinema.Data
{
    public class CinemaDbContext: IdentityDbContext<ApplicationUser>
    {
        public CinemaDbContext()
          : base("CinemaConnectionString", throwIfV1Schema: false)
        {
        }

        public static CinemaDbContext Create()
        {
            return new CinemaDbContext();
        }

        public DbSet<Seance> CinemaSeances { get; set; }
       // public DbSet<Spectator> Spectators { get; set; }
        public DbSet<SeanceSpectator> SeanceSpectators { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Seance>().HasMany(c => c.Spectators)
            //    .WithMany(s => s.Seances)
            //    .Map(t => t.MapLeftKey("SeaceId")
            //    .MapRightKey("SpectatorId")
            //    .ToTable("SeanceSpectator"));

          
        }
    }
}

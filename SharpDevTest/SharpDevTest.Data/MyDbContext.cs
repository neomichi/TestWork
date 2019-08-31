using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SharpDevTest.Data.Model;
using SharpDevTest.Data.Models;



namespace SharpDevTest.Data
{


    public class MyDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> 
       
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        
        }

        public DbSet<HistoryLog> HistorysLog { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {          

            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.Property(u => u.Id).HasDefaultValueSql("newsequentialid()");
            });


        }
    }
}

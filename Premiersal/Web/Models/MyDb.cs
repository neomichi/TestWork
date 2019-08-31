using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Web.Code;

namespace Web.Models
{
    public partial class MyDbContext : DbContext
    {
        public virtual DbSet<Film>  Films { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }


    }
    
}
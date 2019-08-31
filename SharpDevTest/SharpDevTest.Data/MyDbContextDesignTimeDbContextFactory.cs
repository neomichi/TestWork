using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SharpDevTest.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {       
        public MyDbContext CreateDbContext(string[] args)
        {

            //var index = Directory.GetCurrentDirectory().Length - "SharpDevTest.Data".Length;
            //var path = Directory.GetCurrentDirectory().Substring(0, index) + "SharpDevTest.Web";


           

            var builder = new DbContextOptionsBuilder<MyDbContext>()
               .UseSqlServer("Server=N1K0LAY\\SQLEXPRESS;Database=Test3;User Id=sa;Password=!Qwe123;");    
            return new MyDbContext(builder.Options); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Cinema.Data;
using Cinema.Data.Services;
using Cinema.Web.Controllers;
using Cinema.Web.Filters;

namespace Cinema.Web.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {           
            var builder = new ContainerBuilder();
            
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
           
            builder.RegisterType<SeanseService>().As<ISeanseService>();
            builder.RegisterType(typeof(CinemaDbContext));
   
            var config = GlobalConfiguration.Configuration;
    
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(typeof(ApiController).Assembly);
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();
            builder.RegisterType<SpectatorsController>().InstancePerRequest();

        

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
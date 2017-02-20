using Autofac;
using Autofac.Integration.Mvc;
using E_Store.Domain.Abstract;
using E_Store.Domain.Concrete;
using E_Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.WebUI.Infrastructure
{
    public class AutofacConfig
    {
        public static void ConfigrureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<OrderProcessor>().As<IOrderProcessor>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Controllers;
using Autofac;
using WebApplication1.Model;
using WebApplication1.Model.Common;
using WebApplication1.Repository;
using WebApplication1.Repository.Common;
using WebApplication1.Service;
using WebApplication1.Service.Common;
using WebApplication1.WebApi;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            bilder.RegisterType<Person>().As<IPerson>():

            builder.RegisterType<Class1R>().As<IRepositoryCommon>;
            builder.RegisterType<Class1>().As<IService>();

            builder.RegisterModule<DIModuleRepository>();
            builder.RegisterModule<DIModuleService>();

            builder.RegisterApiControllers(AssemblyLoadEventArgs.GetExecutingAssembly());
            
            var container = builder.Build();
            
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}

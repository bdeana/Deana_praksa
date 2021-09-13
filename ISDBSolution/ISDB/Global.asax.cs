using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ISDB.Controllers;
using Autofac;
using ISDB.Common;
using ISDB.Model;
using ISDBRepository.Common;
using ISDB.Repository;
using ISDBService.Common;
using ISDB.Service;
using Autofac.Integration.WebApi;
using ISDBModel.Common;

namespace ISDB
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var config = GlobalConfiguration.Configuration;

            builder.RegisterType<Award>().As<IAward>();
            builder.RegisterType<Album>().As<IAlbum>();
            builder.RegisterType<SongModel>().As<ISongModel>();
            builder.RegisterType<UserModel>().As<IUserModel>();
            builder.RegisterType<ReviewModel>().As<IReviewModel>();


            builder.RegisterModule<RepositoryDIModule>();
            builder.RegisterModule<ServiceDIModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            var container = builder.Build();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}

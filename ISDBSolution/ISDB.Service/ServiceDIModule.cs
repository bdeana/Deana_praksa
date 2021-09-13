using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDB.Service;
using ISDBService.Common;

namespace ISDB.Service
{
    public class ServiceDIModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SongService>().As<ISongService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ReviewService>().As<IReviewService>();
            builder.RegisterType<AlbumService>().As<IAlbumService>();
            builder.RegisterType<AwardService>().As<IAwardService>();
        }
    }
}

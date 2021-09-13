using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISDBRepository.Common;

namespace ISDB.Repository
{
    public class RepositoryDIModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SongRepository>().As<ISongRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ReviewRepository>().As<IReviewRepository>();
            builder.RegisterType<AwardRepository>().As<IAwardRepository>();
            builder.RegisterType<AlbumRepository>().As<IAlbumRepository>();
        }
    }
}

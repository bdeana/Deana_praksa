using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Repository.Common;

namespace WebApplication1.Repository
{
    public class IDModuleRepository : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Class1R>().As<IRepositoryCommon>;
        }

    }
}

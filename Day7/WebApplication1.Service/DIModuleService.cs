using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Service;
using WebApplication1.Service.Common;

namespace WebApplication1.Service
{
    public class DIModuleService : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Class1>().As< IService > ();
        }
    }
}

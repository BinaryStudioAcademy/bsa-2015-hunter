using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject;
using System.Reflection;
using Ninject.Web.WebApi.OwinHost;
using Ninject.Extensions.Conventions;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;
using Hunter.DataAccess.Db;

[assembly: OwinStartup(typeof(Hunter.Rest.Startup))]
namespace Hunter.Rest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);

        }

        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IUserStore<User,int>>().To<HunterUserStore>();
            kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .EndingWith("Repository")
                .BindAllInterfaces()
                );

            kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .EndingWith("Service")
                .BindAllInterfaces()
                );

            return kernel;
        }
    }
}

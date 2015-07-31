using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Pattern.Classes;
using Hunter.Rest;
using Microsoft.AspNet.Identity;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace Hunter.Rest
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>();


                kernel.Bind<IUserStore<User, int>>().To<HunterUserStore>();
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
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

       
    }
}
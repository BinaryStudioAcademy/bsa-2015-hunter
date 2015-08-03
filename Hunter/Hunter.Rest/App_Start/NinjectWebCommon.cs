//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Hunter.DataAccess.Db;
//using Hunter.DataAccess.Interface;
//using Hunter.DataAccess.Interface.Pattern.Classes;
//using Hunter.DataAccess.Interface.Repositories.Classes;
//using Hunter.Rest;
//using Hunter.Services;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security.DataProtection;
//using Microsoft.Web.Infrastructure.DynamicModuleHelper;
//using Ninject;
//using Ninject.Extensions.Conventions;
//using Ninject.Extensions.NamedScope;
//using Ninject.Web.Common;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

//namespace Hunter.Rest
//{
//    public static class NinjectWebCommon
//    {
//        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

//        /// <summary>
//        /// Starts the application
//        /// </summary>
//        public static void Start()
//        {
//            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
//            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
//            bootstrapper.Initialize(CreateKernel);
//        }

//        /// <summary>
//        /// Stops the application.
//        /// </summary>
//        public static void Stop()
//        {
//            bootstrapper.ShutDown();
//        }

//        /// <summary>
//        /// Creates the kernel that will manage your application.
//        /// </summary>
//        /// <returns>The created kernel.</returns>
//        private static IKernel CreateKernel()
//        {
//            var kernel = new StandardKernel();
//            try
//            {
//                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
//                kernel.Bind<IUserStore<User, int>>().To<HunterUserStore>();
//                kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();

//                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
//                kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InSingletonScope();
//                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

                

//                #region Repositories
//                kernel.Bind<IActivityRepository>().To<ActivityRepository>();
//                kernel.Bind<ICandidateRepository>().To<CandidateRepository>();
//                kernel.Bind<ICardRepository>().To<CardRepository>();
//                kernel.Bind<IFeedbackRepository>().To<FeedbackRepository>();
//                kernel.Bind<IInterviewRepository>().To<InterviewRepository>();
//                kernel.Bind<IPoolRepository>().To<PoolRepository>();
//                kernel.Bind<IResumeRepository>().To<ResumeRepository>();
//                kernel.Bind<ISpecialNoteRepository>().To<SpecialNoteRepository>();
//                kernel.Bind<ITestRepository>().To<TestRepository>();
//                kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
//                kernel.Bind<IUserRepository>().To<UserRepository>();
//                kernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();
//                kernel.Bind<IVacancyRepository>().To<VacancyRepository>();
//                #endregion
                


//                #region Services
//                kernel.Bind<IUserService>().To<UserService>();


//                #endregion

//                return kernel;
//            }
//            catch
//            {
//                kernel.Dispose();
//                throw;
//            }
//        }

       
//    }
//}
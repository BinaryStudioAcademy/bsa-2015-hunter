using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject;
using System.Reflection;
using System.Web;
using Ninject.Web.WebApi.OwinHost;
using Ninject.Extensions.Conventions;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Pattern.Classes;
using Hunter.DataAccess.Interface.Repositories.Classes;
using Hunter.Services;
using Microsoft.Owin.Logging;
using Ninject.Web.Common;
using Hunter.Common.Concrete;
using Hunter.Services.Services;
using Hunter.Services.Services.Interfaces;

[assembly: OwinStartup(typeof(Hunter.Rest.Startup))]

namespace Hunter.Rest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureOAuthToken(app);
        }

        public static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IUserStore<User, int>>().To<HunterUserStore>();

            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            #region Repositories
            kernel.Bind<IActivityRepository>().To<ActivityRepository>();
            kernel.Bind<ICandidateRepository>().To<CandidateRepository>();
            kernel.Bind<ICardRepository>().To<CardRepository>();
            kernel.Bind<IFeedbackRepository>().To<FeedbackRepository>();
            kernel.Bind<IInterviewRepository>().To<InterviewRepository>();
            kernel.Bind<IPoolRepository>().To<PoolRepository>();
            kernel.Bind<IResumeRepository>().To<ResumeRepository>();
            kernel.Bind<ISpecialNoteRepository>().To<SpecialNoteRepository>();
            kernel.Bind<ITestRepository>().To<TestRepository>();
            kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();
            kernel.Bind<IVacancyRepository>().To<VacancyRepository>();
            #endregion

            #region Services
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IPoolService>().To<PoolService>();

            #endregion

            kernel.Bind<Common.Interfaces.ILogger>().To<Logger>();

            return kernel;
        }
    }
}
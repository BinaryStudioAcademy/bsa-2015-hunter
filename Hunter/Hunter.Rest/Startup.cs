using Hunter.DataAccess.Db.Base;
using Hunter.DataAccess.Interface.Base;
using Hunter.DataAccess.Interface.Repositories;
using Hunter.Services.Interfaces;
using Microsoft.Owin;
using Owin;
using Ninject;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Hunter.DataAccess.Db;
using Hunter.DataAccess.Interface;
using Hunter.Services;
using Ninject.Web.Common;
using Hunter.Common.Concrete;
using Hunter.DataAccess.Db.Repositories;
using Hunter.DataAccess.Entities;
using Hunter.Rest.Providers;
using Hunter.Services.Services;
using Hunter.Services.Services.Interfaces;
using Hunter.Tools.LinkedIn;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.OwinHost;

[assembly: OwinStartup(typeof(Hunter.Rest.Startup))]

namespace Hunter.Rest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNinjectMiddleware(NinjectContainer.CreateKernel);
          //  ConfigureAuth(app);
            ConfigureOAuthToken(app);
        }
     }


    public class NinjectContainer
    {
        public static IKernel kernel;

        static NinjectContainer()
        {
            kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            // kernel.Bind<IUserStore<User, int>>().To<HunterUserStore>();

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
            //kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleMappingRepository>().To<RoleMappingRepository>();
            kernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();
            kernel.Bind<IVacancyRepository>().To<VacancyRepository>();
            kernel.Bind<IFileRepository>().To<FileRepository>();
            kernel.Bind<IScheduledNotificationRepository>().To<ScheduledNotificationRepository>();
            #endregion

            #region Services
            //kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IPoolService>().To<PoolService>();
            kernel.Bind<ICandidateService>().To<CandidateService>();
            kernel.Bind<IActivityService>().To<ActivityService>();
            kernel.Bind<IVacancyService>().To<VacancyService>();
            kernel.Bind<IUserRoleService>().To<UserRoleService>();
            kernel.Bind<IUserProfileService>().To<UserProfileService>();
            kernel.Bind<IActivityPostService>().To<ActivityPostService>();
            kernel.Bind<IFileService>().To<FileService>();
            kernel.Bind<ITestService>().To<TestService>();
            kernel.Bind<IFeedbackService>().To<FeedbackService>();
            kernel.Bind<ISpecialNoteService>().To<SpecialNoteService>();
            kernel.Bind<IResumeService>().To<ResumeService>();
            kernel.Bind<IActivityHelperService>().To<ActivityHelperService>();
            kernel.Bind<ICardService>().To<CardService>();
            kernel.Bind<IScheduledNotificationService>().To<ScheduledNotificationService>();
            kernel.Bind<IRoleMappingService>().To<RoleMappingService>().InSingletonScope();
            #endregion

            kernel.Bind<Common.Interfaces.ILogger>().To<Logger>();
            kernel.Bind<IPublicPageParser>().To<PublicPageParser>();

        }
        public static IKernel CreateKernel()
        {
            return kernel;
        }

        public static T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}
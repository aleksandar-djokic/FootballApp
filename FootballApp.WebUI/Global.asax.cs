using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using FootballApp.Domain.Abstract;
using FootballApp.Domain.Concrete;

namespace FootballApp.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {   //Autofac DI setup
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //RegisterDependency
            builder.RegisterType<TournamentRepository>().As<ITournamentRepository>();
            builder.RegisterType<SupportRepository>().As<ISupportRepository>();
            builder.RegisterType<NewsRepository>().As<INewsRepository>();
            builder.RegisterType<NotificationRepository>().As<INotificationRepository>();
            builder.RegisterType<PrivateChatRepository>().As<IPrivateChatRepository>();
            builder.RegisterType<TeamRepository>().As<ITeamRepository>();
            builder.RegisterType<TeamChatRepository>().As<ITeamChatRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<FriendsRepository>().As<IFriendsRepository>();
            builder.RegisterType<FreeAgentRepository>().As<IFreeAgentRepository>();
            builder.RegisterType<MatchRepository>().As<IMatchRepository>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

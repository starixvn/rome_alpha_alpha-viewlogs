namespace Alpha.ViewLogs.Site.App_Start
{
    using Controllers;
    using DataAccesses;
    using DataAccesses.Impl;
    using Fanex.Data.Repository;
    using MediatR;
    using Services;
    using Services.Impl;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sunplus.Agent.Model.Entities;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.SessionState;

    public static class DependencyConfig
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Reviewed")]
        public static void Init()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterMediator(container);

            RegisterCrossCuttingConcerns(container);

            RegisterDataAccesses(container);

            RegisterServices(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterMediator(Container container)
        {
            var assemblies = GetAssemblies();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(BaseController).Assembly;
        }

        private static void RegisterCrossCuttingConcerns(Container container)
        {
        }

        private static void RegisterServices(Container container)
        {
            //// Singleton
            container.Register<CachingManager, CachingManagerImpl>(Lifestyle.Singleton);

            //// Transient
            container.Register(
                () =>
                {
                    if (HttpContext.Current != null
                        && (HttpContext.Current.Handler is IRequiresSessionState
                        || HttpContext.Current.Handler is IReadOnlySessionState))
                    {
                        return UserSession.Current;
                    }

                    return new UserSessionBase(string.Empty);
                },
                Lifestyle.Transient);

            container.Register<CustomerService, CustomerServiceImpl>(Lifestyle.Transient);
            container.Register<BaseLogService, BaseLogServiceImpl>(Lifestyle.Transient);
            container.Register<TransformTrackingValueToHtmlService, TransformTrackingValueToHtmlServiceImpl>(Lifestyle.Transient);
            container.Register<XsltLogFormatService, XsltLogFormatServiceImpl>(Lifestyle.Transient);
            container.Register<IPService, IPServiceImpl>(Lifestyle.Transient);

            container.Register<CustomerSettingLogsService, CustomerSettingLogsServiceImpl>(Lifestyle.Transient);
            container.Register<StatusLogsService, StatusLogsServiceImpl>(Lifestyle.Transient);
            container.Register<CreditLogsService, CreditLogsServiceImpl>(Lifestyle.Transient);
            container.Register<LoginLogsService, LoginLogsServiceImpl>(Lifestyle.Transient);
        }

        private static void RegisterDataAccesses(Container container)
        {
            container.Register<IDynamicRepository, DynamicRepository>(Lifestyle.Transient);
            container.Register<CustomerDataAccess, CustomerDataAccessImpl>(Lifestyle.Transient);
            container.Register<BaseDataAccess, BaseDataAccessImpl>(Lifestyle.Transient);
            container.Register<LoginLogsDataAccess, LoginLogsDataAccessImpl>(Lifestyle.Transient);
        }
    }
}
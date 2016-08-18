namespace Alpha.ViewLogs.Site.App_Start
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Controllers;
    using DataAccesses;
    using DataAccesses.Impl;
    using Fanex.Data.Repository;
    using Handlers;
    using MediatR;
    using Services;
    using Services.Impl;
    using Services.Impl.Logs;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using Sunplus.Agent.Model.Entities;

    public static class DependencyConfig
    {
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
            container.Register<TransformService, TransformServiceImpl>(Lifestyle.Transient);
            container.Register<XsltLogFormatter, XsltLogFormatterImpl>(Lifestyle.Transient);
            container.Register<IPService, IPServiceImpl>(Lifestyle.Transient);

            container.RegisterConditional<LogService, CustomerSettingServiceImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(CustomerSettingHandler));
            container.RegisterConditional<LogService, StatusServiceImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(StatusHandler));
            container.RegisterConditional<LogService, CreditServiceImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(CreditHandler));
            container.RegisterConditional<LogService, LoginServiceImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(LoginHandler));
        }

        private static void RegisterDataAccesses(Container container)
        {
            container.Register<IDynamicRepository, DynamicRepository>(Lifestyle.Transient);
            container.Register<CustomerDataAccess, CustomerDataAccessImpl>(Lifestyle.Transient);
            container.RegisterConditional<LogDataAccess, LogDataAccessImpl>(Lifestyle.Transient, c =>
            c.Consumer.ImplementationType == typeof(CustomerSettingServiceImpl)
            || c.Consumer.ImplementationType == typeof(StatusServiceImpl)
            || c.Consumer.ImplementationType == typeof(CreditServiceImpl));
            container.RegisterConditional<LogDataAccess, LoginDataAccessImpl>(Lifestyle.Transient, c => c.Consumer.ImplementationType == typeof(LoginServiceImpl));
        }
    }
}
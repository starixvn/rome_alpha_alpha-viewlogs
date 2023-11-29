namespace Alpha.ViewLogs.Site
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Alpha.Web.Helpers;
    using AlphaPlus.Logging;
    using AlphaPlus.Logging.Web;
    using AlphaPlus.User.Models;
    using App_Start;
    using Auth.Filters;
    using Configurations;
    using Fanex.Caching;
    using Fanex.Data;
    using Fanex.Data.SqlServer;
    using Fanex.Logging.Extensions.Database;
    using Fanex.Logging.Extensions.Web;
    using Fanex.RemotingRef;
    using ONEbook.UM;
    using ONEbook.UM.AspNetMVC.Filters;
    using ONEbook.UM.Configurations;
    using ONEbook.UM.Helpers;
    using ONEbook.UM.NETFramework;
    using ONEbook.UM.Services;
    using ONEbook.UM.Transport;
    using Web;
    using Web.Filters;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;
            AlphaPlus.Configurations.ConfigurationManager.Init(new ConfigurationHelper());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyConfig.Init();

            DbSettingProviderManager
                .StartNewSession()
                .WithSqlServer(resourcePath: AppSettings.StoredProcedurePath)
                .Run();

            ShareSession.Init();

            LoggingConfig.ExceptionHandler = new ExceptionHandlerAdapter();
            LoggingConfig.Init(getDbExceptionInfo: DatabaseInfoGetters.GetDbExceptionInfo);

            var umConfig = new UMClientConfiguration().Initialize();
            UMClientManager
                .UseConfig(umConfig)
                .UseCacheService(new UMCacheService(new CacheService()))
                .UseRestClient(new UMRestClient(umConfig))
                .UseHttpContext(new ONEbook.UM.Helpers.HttpContextHelper());
            GlobalFilters.Filters.Add(new CheckUMFilterAttribute(
                new MaintenanceService(UMClientManager.RestClient, UMClientManager.CacheService, UMClientManager.UMClientConfigs, UMClientManager.HttpContext), null));

            GlobalFilters.Filters.Add(new CheckBelongingFilter());
            GlobalFilters.Filters.Add(new NoCacheFilter());
            GlobalFilters.Filters.Add(new AfterLoginFlowFilter());
            GlobalFilters.Filters.Add(new ProtectedModeFilter());

            RefDataTypeContainer
                .ConnectTo(new Uri(AppSettings.RemoteRefURL))
                .FromApplication("Alpha.ViewLogs.Site");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Request.IsLocal)
            {
                return;
            }

            Exception ex = Server.GetLastError();
            var currentCustomer = UserSession.Current;

            ExceptionHandler.Handle(ex,
                new Dictionary<string, object>
                {
                    ["CustId"] = currentCustomer?.CustId,
                    ["CustName"] = currentCustomer?.UserName,
                    ["SubAccountName"] = currentCustomer?.SubUserName,
                    ["AdminName"] = currentCustomer?.AdminName
                });
        }
    }
}
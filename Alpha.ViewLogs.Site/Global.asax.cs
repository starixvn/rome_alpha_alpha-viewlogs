namespace Alpha.ViewLogs.Site
{
    using App_Start;
    using Auth.Web;
    using ONEbook.UM.Filters;
    using Sunplus.Agent.Utilities;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyConfig.Init();

            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();

            ShareSession.Init();

            GlobalFilters.Filters.Add(new CheckUMFilter());

            Logs.WriteInfo("Alpha - View Logs - Application Start");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            var isIgnoreError = Request.IsLocal ||
                ex is HttpException ||
                ex.Message.Contains("Extend Token Error UserID");

            if (!isIgnoreError)
            {
                Logs.FilterInfoWarnOrError(ex);
            }
        }

        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "shutDownStack", Justification = "Reviewed!")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "shutDownMessage", Justification = "Reviewed!")]
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "Sunplus.Agent.Utilities.Logs.WriteInfo(System.String)", Justification = "Reviewed!")]
        protected void Application_End()
        {
            HttpRuntime runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember(
                "_theRuntime",
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField,
                null,
                null,
                null);

            if (runtime == null)
            {
                return;
            }

            string shutDownMessage = (string)runtime.GetType().InvokeMember(
                "_shutDownMessage",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                null,
                runtime,
                null);

            string shutDownStack = (string)runtime.GetType().InvokeMember(
                "_shutDownStack",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                null,
                runtime,
                null);

            var logMessage = string.Format(
                "\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}",
                shutDownMessage,
                shutDownStack);

            Logs.WriteInfo(logMessage);
        }
    }
}
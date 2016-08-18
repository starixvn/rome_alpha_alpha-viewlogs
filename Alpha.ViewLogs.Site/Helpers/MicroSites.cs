namespace Alpha.ViewLogs.Site.Helpers
{
    using System.Web;
    using Configurations;

    public static class MicroSites
    {
        public static string OldMainPath(string virtualPath)
        {
            return AppPathHelper.GetAppPath(AppSettings.OldMainAppPath, virtualPath);
        }

        public static string ViewLogsPath(string virtualPath)
        {
            return AppPathHelper.GetAppPath(HttpRuntime.AppDomainAppVirtualPath, virtualPath);
        }
    }
}

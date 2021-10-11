namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Web;
    using Sunplus.Agent.Model.Entities;

    public class ViewLogsStyles : BaseStaticResources
    {
        public static string GetAbsolutePath(string relativePath)
            => VirtualPathUtility.ToAbsolute($"{relativePath}{UserSession.Current.DarkModeName}{MinText}.css?v={StaticResourcesVersion}");
    }
}
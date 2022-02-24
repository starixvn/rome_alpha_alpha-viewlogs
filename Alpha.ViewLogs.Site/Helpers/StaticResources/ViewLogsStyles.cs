namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Web;

    public class ViewLogsStyles : BaseStaticResources
    {
        public static string GetAbsolutePath(string relativePath)
            => VirtualPathUtility.ToAbsolute($"{relativePath}{GetDarkModeName()}{MinText}.css?v={StaticResourcesVersion}");
            //=> VirtualPathUtility.ToAbsolute($"{relativePath}{UserSession.Current.DarkModeName}{MinText}.css?v={StaticResourcesVersion}");
    }
}
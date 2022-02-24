namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;
    using Sunplus.Agent.Model.Entities;
    using Web.Configurations;

    public class AssetStyles : BaseStaticResources
    {
        public static IHtmlString Render(params string[] paths)
        {
            var currentUserSession = UserSession.Current;

            return Styles.Render(paths.Select(path => $"{AppSettings.AssetAppPath}{path}{GetDarkModeName()}{MinText}.css?v={StaticResourcesVersion}").ToArray());
            //return Styles.Render(paths.Select(path => $"{currentUserSession.AssetAppPath}{path}{darkModeName}{MinText}.css?v={StaticResourcesVersion}").ToArray());
        }
    }
}
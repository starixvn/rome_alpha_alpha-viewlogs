namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;
    using Configurations;
    using Sunplus.Agent.Model.Entities;

    public class AssetStyles : BaseStaticResources
    {
        public static IHtmlString Render(params string[] paths)
            => Styles.Render(paths.Select(path => $"{AppSettings.AssetAppPath}{path}{UserSession.Current.DarkModeName}{MinText}.css?v={StaticResourcesVersion}").ToArray());
    }
}
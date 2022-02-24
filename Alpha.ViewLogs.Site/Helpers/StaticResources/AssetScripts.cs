namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;
    using Web.Configurations;

    public class AssetScripts : BaseStaticResources
    {
        public static IHtmlString Render(params string[] paths)
            => Scripts.Render(paths.Select(path => $"{AppSettings.AssetAppPath}{path}{MinText}.js?v={StaticResourcesVersion}").ToArray());
            //=> Scripts.Render(paths.Select(path => $"{UserSession.Current.AssetAppPath}{path}{MinText}.js?v={StaticResourcesVersion}").ToArray());
    }
}
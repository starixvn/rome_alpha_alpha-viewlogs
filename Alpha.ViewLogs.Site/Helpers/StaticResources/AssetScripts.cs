namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;

    public class AssetScripts : BaseStaticResources
    {
        public static IHtmlString Render(params string[] paths)
            => Scripts.Render(paths.Select(path => $"{GetAssetAppPath()}{path}{MinText}.js?v={StaticResourcesVersion}").ToArray());
    }
}
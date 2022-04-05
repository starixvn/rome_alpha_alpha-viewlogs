namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;
    using Sunplus.Agent.Model.Entities;

    public class AssetStyles : BaseStaticResources
    {
        public static IHtmlString Render(params string[] paths)
        {
            var userSession = UserSession.Current;

            return Styles.Render(paths.Select(path => $"{GetAssetAppPath(userSession)}{path}{GetDarkModeName(userSession)}{MinText}.css?v={StaticResourcesVersion}").ToArray());
        }
    }
}
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
            var currentUserSession = UserSession.Current;

            return Styles.Render(paths.Select(path => $"{currentUserSession.AssetAppPath}{path}{currentUserSession.DarkModeName}{MinText}.css?v={StaticResourcesVersion}").ToArray());
        }
    }
}
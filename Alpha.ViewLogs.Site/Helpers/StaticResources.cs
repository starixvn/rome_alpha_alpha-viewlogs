namespace Alpha.ViewLogs.Site.Helpers
{
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;
    using Configurations;

    public static class StaticResources
    {
        private static readonly string MinText = ".min";

        static StaticResources()
        {
            if (IsDebugMode())
            {
                MinText = string.Empty;
            }
        }

        private static bool IsDebugMode()
        {
            var compilationSection = (CompilationSection)ConfigurationManager.GetSection(@"system.web/compilation");

            return compilationSection.Debug;
        }

        #region Assets App

        public static class AssetStyles
        {
            public static IHtmlString Render(params string[] paths)
            {
                return System.Web.Optimization.Styles.Render(paths.Select(path => $"{AppSettings.AssetAppPath}{path}{MinText}.css").ToArray());
            }
        }

        public static class AssetScripts
        {
            public static IHtmlString Render(params string[] paths)
            {
                return System.Web.Optimization.Scripts.Render(paths.Select(path => $"{AppSettings.AssetAppPath}{path}{MinText}.js").ToArray());
            }
        }

        #endregion Assets App

        #region Current site

        public static string ViewLogsSiteScriptUrl(string relativePath)
        {
            return VirtualPathUtility.ToAbsolute($"{relativePath}{MinText}.js");
        }

        public static string ViewLogsSiteStyleUrl(string relativePath)
        {
            return VirtualPathUtility.ToAbsolute($"{relativePath}{MinText}.css");
        }

        #endregion
    }
}
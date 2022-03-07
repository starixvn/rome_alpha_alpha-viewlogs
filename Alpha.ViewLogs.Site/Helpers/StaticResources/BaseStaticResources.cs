namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Configuration;
    using Sunplus.Agent.Model.Entities;
    using Web.Configurations;

    public class BaseStaticResources
    {
        public static readonly string MinText = ".min";

        public static readonly string StaticResourcesVersion;

        static BaseStaticResources()
        {
            if (IsDebugMode())
            {
                MinText = string.Empty;
            }

            StaticResourcesVersion = GetBuildVersion();
        }

        private static bool IsDebugMode()
        {
            var compilationSection = (CompilationSection)ConfigurationManager.GetSection(@"system.web/compilation");

            return compilationSection.Debug;
        }

        private static string GetBuildVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            return versionInfo.FileVersion;
        }

        protected static string GetDarkModeName(IUserSession userSession = null)
            => AppSettings.IsOfferDarkMode
            ? (userSession ?? UserSession.Current).DarkModeName
            : string.Empty;

        protected static string GetAssetAppPath(IUserSession userSession = null)
        {
            var assetAppPath = (userSession ?? UserSession.Current).AssetAppPath;

            return string.IsNullOrWhiteSpace(assetAppPath)
                ? AppSettings.AssetAppPath
                : assetAppPath;
        }
    }
}
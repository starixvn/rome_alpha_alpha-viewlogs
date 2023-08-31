namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using System.Web.Configuration;
    using AlphaPlus.User.Models;

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
            => (userSession ?? UserSession.Current).DarkModeName;

        protected static string GetAssetAppPath(IUserSession userSession = null)
        {
            var assetAppPath = (userSession ?? UserSession.Current).AssetAppPath;

            return string.IsNullOrWhiteSpace(assetAppPath)
                ? AlphaPlus.Configurations.AppSettings.AssetAppPath
                : assetAppPath;
        }
    }
}
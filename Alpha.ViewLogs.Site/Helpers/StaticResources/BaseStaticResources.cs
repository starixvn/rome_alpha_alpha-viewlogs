namespace Alpha.ViewLogs.Site.Helpers.StaticResources
{
    using System.Configuration;
    using System.Web.Configuration;

    public class BaseStaticResources
    {
        public static readonly string MinText = ".min";

        static BaseStaticResources()
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
    }
}
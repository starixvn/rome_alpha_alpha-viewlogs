namespace Alpha.ViewLogs.Configurations
{
    using System.Configuration;

    public static class AppSettings
    {
        public static string OldMainAppPath
        {
            get
            {
                return GetAppPath("OldMainAppPath");
            }
        }

        public static string GetAppPath(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}

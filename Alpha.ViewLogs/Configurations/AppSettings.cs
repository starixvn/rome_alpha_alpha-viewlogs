namespace Alpha.ViewLogs.Configurations
{
    using Fanex.Common;
    using System.Configuration;

    public static class AppSettings
    {
        public static string OldMainAppPath
        {
            get
            {
                return GetSettingValue("OldMainAppPath");
            }
        }

        public static string AccessDeniedPage
        {
            get
            {
                return GetSettingValue("AccessDeniedPage");
            }
        }

        public static bool IsDebug
        {
            get
            {
                var isDebugSetting = Converter.ToBool(GetSettingValue("IsDebug"));
                return isDebugSetting;
            }
        }

        public static string GetSettingValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}

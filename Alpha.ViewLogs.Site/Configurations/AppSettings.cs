namespace Alpha.ViewLogs.Site.Configurations
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public static class AppSettings
    {
        public static string VirtualDirectory
        {
            get
            {
                return GetConfigValue("VirtualDirectory");
            }
        }

        public static string OldMainAppPath
        {
            get
            {
                return GetConfigValue("OldMainAppPath");
            }
        }

        public static string AccessDeniedPage
        {
            get
            {
                return GetConfigValue("AccessDeniedPage");
            }
        }

        public static List<int> ActionIds_Racing_151_31
        {
            get
            {
                var settingValue = GetConfigValue("ActionIds_Racing_151_31");
                return GetActionIds(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_151_41
        {
            get
            {
                var settingValue = GetConfigValue("ActionIds_Racing_151_41");
                return GetActionIds(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_151_51
        {
            get
            {
                var settingValue = GetConfigValue("ActionIds_Racing_151_51");
                return GetActionIds(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_152_41
        {
            get
            {
                var settingValue = GetConfigValue("ActionIds_Racing_152_41");
                return GetActionIds(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_152_51
        {
            get
            {
                var settingValue = GetConfigValue("ActionIds_Racing_152_51");
                return GetActionIds(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_153_51
        {
            get
            {
                var settingValue = GetConfigValue("ActionIds_Racing_153_51");
                return GetActionIds(settingValue);
            }
        }

        public static string AssetAppPath
        {
            get
            {
                return GetConfigValue("AssetAppPath");
            }
        }

        public static string GetConfigValue(string configName)
        {
            return ConfigurationManager.AppSettings[configName];
        }

        private static List<int> GetActionIds(string actionIdsString)
        {
            var actionIds = new List<int>();
            if (string.IsNullOrWhiteSpace(actionIdsString))
            {
                return actionIds;
            }

            actionIds = actionIdsString.Split(',')
                .Select(t => int.Parse(t))
                .ToList();

            return actionIds;
        }
    }
}

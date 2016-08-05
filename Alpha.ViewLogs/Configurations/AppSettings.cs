namespace Alpha.ViewLogs.Configurations
{
    using Fanex.Common;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

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

        public static List<int> ActionIds_Racing_151_31
        {
            get
            {
                var settingValue = GetSettingValue("ActionIds_Racing_151_31");
                return GetIdListFromStringSetting(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_151_41
        {
            get
            {
                var settingValue = GetSettingValue("ActionIds_Racing_151_41");
                return GetIdListFromStringSetting(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_151_51
        {
            get
            {
                var settingValue = GetSettingValue("ActionIds_Racing_151_51");
                return GetIdListFromStringSetting(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_152_41
        {
            get
            {
                var settingValue = GetSettingValue("ActionIds_Racing_152_41");
                return GetIdListFromStringSetting(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_152_51
        {
            get
            {
                var settingValue = GetSettingValue("ActionIds_Racing_152_51");
                return GetIdListFromStringSetting(settingValue);
            }
        }

        public static List<int> ActionIds_Racing_153_51
        {
            get
            {
                var settingValue = GetSettingValue("ActionIds_Racing_153_51");
                return GetIdListFromStringSetting(settingValue);
            }
        }

        public static List<int> NewFormatActionLogIds
        {
            get
            {
                var settingValue = GetSettingValue("NewFormatActionLogIds");
                return GetIdListFromStringSetting(settingValue);
            }
        }

        public static string AssetAppPath
        {
            get
            {
                return GetSettingValue("AssetAppPath");
            }
        }

        public static string GetSettingValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        private static List<int> GetIdListFromStringSetting(string settingValue)
        {
            var idList = new List<int>();
            if (string.IsNullOrEmpty(settingValue))
            {
                return idList;
            }

            idList = settingValue.Split(',')
                .Select(t => int.Parse(t))
                .ToList();

            return idList;
        }
    }
}

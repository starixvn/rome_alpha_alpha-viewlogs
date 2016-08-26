namespace Alpha.ViewLogs.Site.Configurations
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    public static class AppSettings
    {
        private static readonly object LockObject = new object();

        private static Dictionary<string, int> statusMappings;

        private static IEnumerable<int> actionIdsRacing15131;
        private static IEnumerable<int> actionIdsRacing15141;
        private static IEnumerable<int> actionIdsRacing15151;
        private static IEnumerable<int> actionIdsRacing15241;
        private static IEnumerable<int> actionIdsRacing15251;
        private static IEnumerable<int> actionIdsRacing15351;

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

        public static IEnumerable<int> ActionIds_Racing_151_31
        {
            get
            {
                if (actionIdsRacing15131 == null)
                {
                    actionIdsRacing15131 = ParseActionIds(GetConfigValue("ActionIds_Racing_151_31"));
                }

                return actionIdsRacing15131;
            }
        }

        public static IEnumerable<int> ActionIds_Racing_151_41
        {
            get
            {
                if (actionIdsRacing15141 == null)
                {
                    actionIdsRacing15141 = ParseActionIds(GetConfigValue("ActionIds_Racing_151_41"));
                }

                return actionIdsRacing15141;
            }
        }

        public static IEnumerable<int> ActionIds_Racing_151_51
        {
            get
            {
                if (actionIdsRacing15151 == null)
                {
                    actionIdsRacing15151 = ParseActionIds(GetConfigValue("ActionIds_Racing_151_51"));
                }

                return actionIdsRacing15151;
            }
        }

        public static IEnumerable<int> ActionIds_Racing_152_41
        {
            get
            {
                if (actionIdsRacing15241 == null)
                {
                    actionIdsRacing15241 = ParseActionIds(GetConfigValue("ActionIds_Racing_152_41"));
                }

                return actionIdsRacing15241;
            }
        }

        public static IEnumerable<int> ActionIds_Racing_152_51
        {
            get
            {
                if (actionIdsRacing15251 == null)
                {
                    actionIdsRacing15251 = ParseActionIds(GetConfigValue("ActionIds_Racing_152_51"));
                }

                return actionIdsRacing15251;
            }
        }

        public static IEnumerable<int> ActionIds_Racing_153_51
        {
            get
            {
                if (actionIdsRacing15351 == null)
                {
                    actionIdsRacing15351 = ParseActionIds(GetConfigValue("ActionIds_Racing_153_51"));
                }

                return actionIdsRacing15351;
            }
        }

        public static string AssetAppPath
        {
            get
            {
                return GetConfigValue("AssetAppPath");
            }
        }

        /// <summary>
        /// Key: statusName. Value: productId
        /// </summary>
        public static Dictionary<string, int> StatusMappings
        {
            get
            {
                if (statusMappings == null)
                {
                    InitStatusMappings();
                }

                return statusMappings;
            }
        }

        public static string StoreProcedurePath
        {
            get
            {
                return GetConfigValue("StoreProcedurePath");
            }
        }
        
        public static string GetConfigValue(string configName)
        {
            return ConfigurationManager.AppSettings[configName];
        }

        private static IEnumerable<int> ParseActionIds(string actionIdsString)
        {
            if (string.IsNullOrWhiteSpace(actionIdsString))
            {
                return Enumerable.Empty<int>();
            }

            return actionIdsString.Split(',')
                .Select(t => int.Parse(t));
        }

        private static void InitStatusMappings()
        {
            lock (LockObject)
            {
                if (statusMappings == null)
                {
                    statusMappings = new Dictionary<string, int>();
                    var statusMappingString = GetConfigValue("ViewLogs:StatusMapping");

                    if (string.IsNullOrWhiteSpace(statusMappingString))
                    {
                        return;
                    }

                    statusMappingString.Split(',')
                        .ToList()
                        .ForEach((combinedValue) =>
                        {
                            //// format: statusName:productId
                            var keyValue = combinedValue.Split(':');

                            if (keyValue.Length > 1)
                            {
                                statusMappings.Add(keyValue[0].ToLower(), int.Parse(keyValue[1]));
                            }
                        });
                }
            }
        }
    }
}
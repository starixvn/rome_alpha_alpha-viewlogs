namespace Sunplus.Agent.UI._ViewLog
{
	using Alpha.ViewLogs.LocalResources;
	using Alpha.ViewLogs.Models;
	using Alpha.ViewLogs.Services;
	using Sunplus.Agent.Model.BL.Cache;
	using Sunplus.Agent.Model.Entities;
	using Sunplus.Agent.Model.Entities.Permission;
	using System.Collections.Generic;
	using System.Net;

	public class XsltLogFormat
    {
        private string RESOURCE_FILE_NAME = "ViewLog";

        public XsltLogFormat()
        {
            if (_visibilities == null)
            {
                _visibilities = new Dictionary<string, bool>
                {
                    {"151_31", UserPermission.IsShowRacing_151_31},
                    {"151_41", UserPermission.IsShowRacing_151_41},
                    {"151_51", UserPermission.IsShowRacing_151_51},
                    {"152_41", UserPermission.IsShowRacing_152_41},
                    {"152_51", UserPermission.IsShowRacing_152_51},
                    {"153_51", UserPermission.IsShowRacing_153_51},
                    {"161_90", UserPermission.IsShowNumberGroup},
                    {"180", UserPermission.IsShowVirtualSports},
                    {"255", UserPermission.IsShowGoldDeluxeSettingForCredit},
                    {"204", UserPermission.IsShowColossusBets},
                    {"44", UserPermission.IsShowSportsbook2}
                };
            }
        }

        private Dictionary<string, bool> _visibilities = null;

        public bool IsVisible(string key)
        {
            return _visibilities[key];
        }

        public static IUserSession CurrentUserSession
        {
            get
            {
                return UserSession.Current;
            }
        }

        public string GetLogResource(string key)
        {
            return getResource(key);
        }

        public string GetSportName(int sporttype)
        {
            return CacheRef.GetSportName(sporttype);
        }

        public string GetBettypeName(int bettype)
        {
            return CacheRef.GetBettype(bettype);
        }

        private string CovertCodeCondition(string code)
        {
            char[] c = code.Trim().ToCharArray();
            string condition = string.Empty;
            string hyphen = " - ";
            if (c.Length > 0)
            {
                if (c[7].ToString() == "0") // daily
                {
                    return Lang.daily;
                }
                else
                {
                    if (c[0] == '1') condition += Lang.sun + hyphen;
                    if (c[1] == '1') condition += Lang.mon + hyphen;
                    if (c[2] == '1') condition += Lang.tue + hyphen;
                    if (c[3] == '1') condition += Lang.wed + hyphen;
                    if (c[4] == '1') condition += Lang.thurs + hyphen;
                    if (c[5] == '1') condition += Lang.fri + hyphen;
                    if (c[6] == '1') condition += Lang.sat + hyphen;

                    return Lang.weekly + " (" + condition.Remove(condition.Length - 3, 3) + ")";
                }
            }
            return condition;
        }

        public string TransferConditionFromCode(string code)
        {
            if (string.IsNullOrEmpty(code) || code == " ") return string.Empty;
            if (code.IndexOf("</div>") != -1)
            {
                string[] arr = code.Split("</div>".ToCharArray());
                string valuechange = arr[23];
                string valueold = arr[4];
                code = code.Replace(valuechange, CovertCodeCondition(valuechange));
                code = code.Replace(valueold, CovertCodeCondition(valueold));
                return code;
            }
            return CovertCodeCondition(code);
        }

        public static string FormatIp(string ip)
        {
            if (!ValidateIpAddress(ip))
            {
                return string.Empty;
            }

            if (IPHelper.IsVnIp(ip) && !CurrentUserSession.IsAdminCanViewVNIP)
            {
                return string.Empty;
            }

            //return IPHelper.FormatIp(ip);
			return string.Format("<a class=\"iplink\" href=\"javascript:OpenIPInfo('{0}');\">{0}</a>", ip);
		}

        public static string FormatCountryCode(string ip, string country)
        {
            if (!ValidateIpAddress(ip))
            {
                return string.Empty;
            }

            if (IPHelper.IsVnIp(ip) && !CurrentUserSession.IsAdminCanViewVNIP)
            {
                return string.Empty;
            }

            return country;
        }

        public static string FormatBettypeID(string bettypeId)
        {
            return CacheRef.GetBettype((object)bettypeId);
        }

        public static string FormatCodeSite(string code)
        {
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(code))
            {
                if (code == "2")
                {
                    condition = Alpha.ViewLogs.LocalResources.Viewlog.agentSite;
                }
                else if (code == "1")
                {
                    condition = Alpha.ViewLogs.LocalResources.Viewlog.accountingSite;
                }
            }
            return condition;
        }

        public static string FormatErrCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return "0";

            return code;
        }

        public static string FormatErrMsg(string code)
        {
            if (string.Compare(code, "no information is available") == 0)
            {
                code = "";
            }

            return code;
        }

        public static string FormatMemberWinLimit(string value)
        {
            return value.Replace("x", "X").Replace("unlimited", Alpha.ViewLogs.LocalResources.Lang.Unlimited);
        }

        public static string FormatUnlimited(int unlimited)
        {
            return unlimited == -1 ? Alpha.ViewLogs.LocalResources.Lang.Unlimited : unlimited.ToString();
        }

        public string GetSportWithConditionHtml(
            string disableVitualSports,
            string disableGoldDeluxe,
            string disableColossusBets)
        {
            var html = string.Empty;
            var visibleSports = new Dictionary<string, SportDisplayInfo>();
            var sportId = "180";
            visibleSports.Add(sportId, new SportDisplayInfo
            {
                IsVisible = _visibilities[sportId],
                DisplayName = Alpha.ViewLogs.LocalResources.Viewlog.disabledVS,
                DisplayValue = disableVitualSports
            });

            sportId = "255";
            visibleSports.Add(sportId, new SportDisplayInfo
            {
                IsVisible = _visibilities[sportId],
                DisplayName = Alpha.ViewLogs.LocalResources.Viewlog.disableGoldDeluxe,
                DisplayValue = disableGoldDeluxe
            });

            sportId = "204";
            visibleSports.Add(sportId, new SportDisplayInfo
            {
                IsVisible = _visibilities[sportId],
                DisplayName = Alpha.ViewLogs.LocalResources.Viewlog.disableColossusBets,
                DisplayValue = disableColossusBets
            });

            var columnIndex = 0;
            var maxColumnIndex = 2;
            var sportDisplayColumns = "<td class=\"colW1\">{0}</td><td class=\"colW2\">{1}</td>";
            foreach (var visibleSport in visibleSports)
            {
                var sport = visibleSport.Value;
                if (!sport.IsVisible)
                {
                    continue;
                }

                if (columnIndex == 0)
                {
                    html += "<tr>";
                }

                html += string.Format(sportDisplayColumns, sport.DisplayName, sport.DisplayValue);
                columnIndex++;
                if (columnIndex > maxColumnIndex)
                {
                    html += "</tr>";
                    columnIndex = 0;
                }
            }

            for (; columnIndex != 0 && columnIndex <= maxColumnIndex; )
            {
                html += string.Format(sportDisplayColumns, string.Empty, string.Empty);
                columnIndex++;
                if (columnIndex > maxColumnIndex)
                {
                    html += "</tr>";
                }
            }

            return html;
        }

        #region Keno

        public bool IsVisibleKeno(string hideKeno)
        {
            return hideKeno == "0";
        }
        public static string GetDisabledKenoBettypeName(string disableBettype)
        { 
            return string.Format("{0} {1}",
				Alpha.ViewLogs.LocalResources.Viewlog.disabled,
                CacheRef.GetBetTypeName(disableBettype));
        }

        #endregion

        public bool IsShowMyanmarOdds()
        {
            return UserPermission.IsShowMyanmarOdds;
        }

        private static string getResource(string key)
        {
			Alpha.ViewLogs.LocalResources.Viewlog.ResourceManager.IgnoreCase = true;
            return Alpha.ViewLogs.LocalResources.Viewlog.ResourceManager.GetString(key) ?? string.Empty;
        }

        private static bool ValidateIpAddress(string ipAddress)
        {
            IPAddress ip;
            return !string.IsNullOrEmpty(ipAddress) && IPAddress.TryParse(ipAddress, out ip);
        }
    }
}
namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using AppResources;
    using Constants;
    using Helpers;
    using Models;
    using Params;
    using Sunplus.Agent.Model.BL.Cache;

    public class LogFormatterImpl : LogFormatter
    {
        private const string StatusHtmlFormat = "<tr><td colspan=\"3\" class=\"colW1\">{0}</td><td colspan=\"3\" class=\"colW2\">{1}</td></tr>";

        private IPService ipService;
        private Dictionary<string, bool> custStatuses;
        private bool isShowMyanmarOdds;

        public LogFormatterImpl(IPService ipService)
        {
            this.ipService = ipService;
        }

        /// <summary>
        /// Need be reviewed code here, AssignParams should not in formatter class
        /// </summary>
        /// <param name="transformParam"></param>
        public void Init(TransformParam transformParam)
        {
            if (transformParam == null)
            {
                return;
            }

            custStatuses = transformParam.CustStatuses;
            isShowMyanmarOdds = transformParam.IsShowMyanmarOdds;
        }

        public string FormatIP(string ip)
        {
            return ipService.FormatIP(ip);
        }

        public string FormatCountryCode(string ip, string country)
        {
            return ipService.FormatCountryCode(ip, country);
        }

        public bool IsVisible(string key)
        {
            return custStatuses[key];
        }

        public string GetLogResource(string key)
        {
            TrackingValueResources.ResourceManager.IgnoreCase = true;
            return TrackingValueResources.ResourceManager.GetString(key) ?? string.Empty;
        }

        public string GetSportName(int sportType)
        {
            return CacheRef.GetSportName(sportType);
        }

        public string GetBettypeName(int betType)
        {
            return CacheRef.GetBettype(betType);
        }

        public string TransferConditionFromCode(string dayCodesWithDiff)
        {
            if (string.IsNullOrWhiteSpace(dayCodesWithDiff))
            {
                return string.Empty;
            }

            var codes = XElement.Parse("<root>" + dayCodesWithDiff + "</root>").Elements().ToList();
            var nodeCount = codes.Count();

            var newValue = string.Empty;
            var oldValue = string.Empty;

            var nodeCountIncaseOfHavingNewOldDivs = 2;

            if (nodeCount >= nodeCountIncaseOfHavingNewOldDivs)
            {
                newValue = codes.First().Value;

                // oldValue has format: (7 flags for 7 days)
                oldValue = codes.Last().Value.Substring(1, 8);

                dayCodesWithDiff = dayCodesWithDiff.Replace(newValue, CovertCodeCondition(newValue))
                                                   .Replace(oldValue, CovertCodeCondition(oldValue));
                return dayCodesWithDiff;
            }

            var nodeCountIncaseOfUnchangeValuesOrNotShowOldValues = 1;

            if (nodeCount == nodeCountIncaseOfUnchangeValuesOrNotShowOldValues)
            {
                newValue = codes.First().Value;

                // title: "{oldValue} --> {newValue}" or empty
                // old value = Split()[0]
                oldValue = codes.First().Attribute("title").Value.Split()[0];

                dayCodesWithDiff = dayCodesWithDiff.Replace(newValue, CovertCodeCondition(newValue))
                                                   .Replace(oldValue, CovertCodeCondition(oldValue));
                return dayCodesWithDiff;
            }

            return CovertCodeCondition(dayCodesWithDiff);
        }

        public string FormatBetTypeId(string betTypeId)
        {
            return CacheRef.GetBettype((object)betTypeId);
        }

        public string FormatCodeSite(string code)
        {
            var condition = string.Empty;

            if (string.IsNullOrWhiteSpace(code))
            {
                return condition;
            }

            var siteNames = new Dictionary<string, string>();
            siteNames.Add("1", Resources.AccountingSite);
            siteNames.Add("2", Resources.AgentSite);

            if (siteNames.ContainsKey(code))
            {
                condition = siteNames[code];
            }

            return condition;
        }

        public string FormatErrCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return "0";
            }

            return code;
        }

        public string FormatErrMsg(string code)
        {
            if (string.Equals(code, "no information is available", StringComparison.OrdinalIgnoreCase))
            {
                code = string.Empty;
            }

            return code;
        }

        public string FormatMemberWinLimit(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Replace("x", "X").Replace("unlimited", Resources.Unlimited);
        }

        public string FormatUnlimited(int unlimited)
        {
            return unlimited == -1 ? Resources.Unlimited : unlimited.ToString();
        }

        public bool IsVisibleKeno(string hideKeno)
        {
            return hideKeno == "0";
        }

        public string GetDisabledKenoBetTypeName(string disableBetType)
        {
            return string.Format("{0} {1}", Resources.Disabled, CacheRef.GetBetTypeName(disableBetType));
        }

        public bool IsShowMyanmarOdds()
        {
            return isShowMyanmarOdds;
        }

        public bool IsRNGCasino(int sportType)
        {
            return sportType == SportId.RNGGame ||
                sportType == SportId.RNGMiniGame ||
                sportType == SportId.RNGMobile;
        }

        public bool IsGoldDeluxe(int sportType)
        {
            return sportType == SportId.GoldDeluxe;
        }

        private string CovertCodeCondition(string dayCodes)
        {
            char[] codes = dayCodes.Trim().ToCharArray();
            string condition = string.Empty;

            if (!codes.Any())
            {
                return condition;
            }

            var indicatorOfWeekly = 7;

            if (codes[indicatorOfWeekly].ToString() == "0")
            {
                return Resources.Daily;
            }

            var selectedDays = new List<string>();
            var daysInWeek = DateTimeHelper.DaysInWeek();
            var numberOfDaysInWeek = daysInWeek.Count();

            for (var i = 0; i < numberOfDaysInWeek; i++)
            {
                if (codes[i] == '1')
                {
                    selectedDays.Add(daysInWeek[i]);
                }
            }

            return Resources.Weekly + " (" + string.Join(" - ", selectedDays) + ")";
        }

        public string RenderCustStatus(string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(name) &&
                custStatuses.ContainsKey(name.ToLower())
                && custStatuses[name.ToLower()])
            {
                return string.Format(StatusHtmlFormat, GetLogResource(name), value);
            }

            return string.Empty;
        }
    }
}
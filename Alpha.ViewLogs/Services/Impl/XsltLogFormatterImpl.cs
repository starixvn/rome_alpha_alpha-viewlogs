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

    public class XsltLogFormatterImpl : XsltLogFormatter
    {
        private IPService ipService;

        private Dictionary<string, bool> sportBetTypeVisibilities;
        private bool isShowMyanmarOdds;

        public XsltLogFormatterImpl(IPService ipService)
        {
            this.ipService = ipService;
        }

        /// <summary>
        /// Need be reviewed code here, AssignParams should not in formatter class
        /// </summary>
        /// <param name="transformParam"></param>
        public void AssignParams(TransformParam transformParam)
        {
            if (transformParam == null)
            {
                return;
            }

            sportBetTypeVisibilities = transformParam.SportBetTypeVisibilities;
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
            return sportBetTypeVisibilities[key];
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

            var nodeCountIncaseOfHavingNewOldDivs = 3;

            if (nodeCount == nodeCountIncaseOfHavingNewOldDivs)
            {
                newValue = codes.First().Value;
                oldValue = codes.Last().Value;

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

            if (string.IsNullOrEmpty(code))
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
            if (string.IsNullOrEmpty(code))
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
            return string.IsNullOrEmpty(value) ? string.Empty : value.Replace("x", "X").Replace("unlimited", Resources.Unlimited);
        }

        public string FormatUnlimited(int unlimited)
        {
            return unlimited == -1 ? Resources.Unlimited : unlimited.ToString();
        }

        public string GetSportWithConditionHtml(
            string disableVitualSports,
            string disableGoldDeluxe,
            string disableColossusBets,
            string disableCockfighting,
            string disableLiveCasinoAllbet,
            string disableLiveCasinoVoidbridge)
        {
            var sportDisplays = GetSportDisplays(
                disableVitualSports,
                disableGoldDeluxe,
                disableColossusBets,
                disableCockfighting,
                disableLiveCasinoAllbet,
                disableLiveCasinoVoidbridge);

            var html = string.Empty;
            var columnIndex = 0;
            var maxColumnIndex = 2;
            var sportDisplayColumns = "<td class=\"colW1\">{0}</td><td class=\"colW2\">{1}</td>";

            foreach (var sportDisplay in sportDisplays)
            {
                if (!sportDisplay.IsVisible)
                {
                    continue;
                }

                if (columnIndex == 0)
                {
                    html += "<tr>";
                }

                html += string.Format(sportDisplayColumns, sportDisplay.DisplayName, sportDisplay.DisplayValue);
                columnIndex++;

                if (columnIndex > maxColumnIndex)
                {
                    html += "</tr>";
                    columnIndex = 0;
                }
            }

            for (; columnIndex != 0 && columnIndex <= maxColumnIndex;)
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
            return sportType == Sport.RNGGame ||
                sportType == Sport.RNGMiniGame ||
                sportType == Sport.RNGMobile;
        }

        public bool IsGoldDeluxe(int sportType)
        {
            return sportType == Sport.GoldDeluxe;
        }

        private string CovertCodeCondition(string dayCodes)
        {
            char[] codes = dayCodes.Trim().ToCharArray();
            string condition = string.Empty;

            if (codes.Length == 0)
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

        private List<SportDisplay> GetSportDisplays(
            string disableVitualSports,
            string disableGoldDeluxe,
            string disableColossusBets,
            string disableCockfighting,
            string disableLiveCasinoAllbet,
            string disableLiveCasinoVoidbridge)
        {
            var sportDisplays = new List<SportDisplay>();

            var sportId = Sport.VirtualSports.ToString();
            sportDisplays.Add(new SportDisplay(sportId, sportBetTypeVisibilities[sportId], Resources.DisabledVirtualSports, disableVitualSports));

            sportId = Sport.GoldDeluxe.ToString();
            sportDisplays.Add(new SportDisplay(sportId, sportBetTypeVisibilities[sportId], Resources.DisableGoldDeluxe, disableGoldDeluxe));

            sportId = Sport.ColossusBets.ToString();
            sportDisplays.Add(new SportDisplay(sportId, sportBetTypeVisibilities[sportId], Resources.DisableColossusBets, disableColossusBets));

            sportId = Sport.CockFighting.ToString();
            sportDisplays.Add(new SportDisplay(sportId, sportBetTypeVisibilities[sportId], Resources.DisableCockfighting, disableCockfighting));

            sportId = Sport.AllBet.ToString();
            sportDisplays.Add(new SportDisplay(sportId, sportBetTypeVisibilities[sportId], Resources.DisableAllBet, disableLiveCasinoAllbet));

            sportId = Sport.Voidbridge.ToString();
            sportDisplays.Add(new SportDisplay(sportId, sportBetTypeVisibilities[sportId], Resources.DisableMacauGames, disableLiveCasinoVoidbridge));

            return sportDisplays;
        }
    }
}

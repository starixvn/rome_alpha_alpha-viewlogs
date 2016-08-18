namespace Alpha.ViewLogs.Site.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Configurations;
    using Fanex.Common;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using ViewLogs.Constants;

    public class LogQuery
    {
        public const string CookieShowOldValue = "Alpha.ViewLogs.ShowOldValuesCookie";

        private readonly DateTime maxReportDate;

        private readonly DateTime minReportDate;

        private readonly Dictionary<int, bool> actionIdVisibilities;

        private readonly Dictionary<string, bool> sportBetTypeVisibilities;

        public LogQuery() : this(null, null, null, null)
        {
        }

        public LogQuery(
            IUserSession currentUserSession,
            bool? isShowOldValue,
            Dictionary<int, bool> actionIdVisibilities = null,
            Dictionary<string, bool> sportBetTypeVisibilities = null)
        {
            currentUserSession = currentUserSession ?? UserSession.Current;
            this.IsShowOldValue = isShowOldValue ?? GetShowOldValueFromCookie();
            this.actionIdVisibilities = actionIdVisibilities ?? GetActionIdVisibilitiesFromAppSettings();
            this.sportBetTypeVisibilities = sportBetTypeVisibilities ?? GetSportBetTypeVisibilitiesFromUserPermission();

            // Limit time range for view log in 2 years (start from beginning of last year)
            this.maxReportDate = DateTime.Today;
            this.minReportDate = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1);

            this.RoleId = currentUserSession.RoleId;
            this.HasViewLogPermission = currentUserSession.SubPageAccess != null && currentUserSession.SubPageAccess.ViewLog;
            this.Language = currentUserSession.Language;
            this.IsInternal = false;
            this.UplineId = currentUserSession.CustId;
            this.IsShowMyanmarOdds = UserPermission.IsShowMyanmarOdds;
            this.PreviousUsername = this.Username;
        }

        public string PreviousUsername { get; set; }

        public string Username { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ActionTypeId { get; set; }

        public int UplineId { get; set; }

        public byte RoleId { get; set; }

        public int SubAccId { get; set; }

        public bool HasViewLogPermission { get; set; }

        public bool IsInternal { get; set; }

        public bool IsShowOldValue { get; set; }

        public string Language { get; set; }

        public string PageIndex { get; set; }

        public string PageSize { get; set; }

        public Dictionary<int, bool> ActionIdVisibilities
        {
            get
            {
                return actionIdVisibilities;
            }
        }

        public Dictionary<string, bool> SportBetTypeVisibilities
        {
            get
            {
                return sportBetTypeVisibilities;
            }
        }

        public bool IsShowMyanmarOdds { get; set; }

        public DateTime MaxReportDate
        {
            get
            {
                return maxReportDate;
            }
        }

        public DateTime MinReportDate
        {
            get
            {
                return minReportDate;
            }
        }

        private Dictionary<int, bool> GetActionIdVisibilitiesFromAppSettings()
        {
            var actionIdVisibilities = new Dictionary<int, bool>();
            AppendActionIdVisibilities(actionIdVisibilities, AppSettings.ActionIds_Racing_151_31, UserPermission.IsShowRacing_151_31);
            AppendActionIdVisibilities(actionIdVisibilities, AppSettings.ActionIds_Racing_151_41, UserPermission.IsShowRacing_151_41);
            AppendActionIdVisibilities(actionIdVisibilities, AppSettings.ActionIds_Racing_151_51, UserPermission.IsShowRacing_151_51);
            AppendActionIdVisibilities(actionIdVisibilities, AppSettings.ActionIds_Racing_152_41, UserPermission.IsShowRacing_152_41);
            AppendActionIdVisibilities(actionIdVisibilities, AppSettings.ActionIds_Racing_152_51, UserPermission.IsShowRacing_152_51);
            AppendActionIdVisibilities(actionIdVisibilities, AppSettings.ActionIds_Racing_153_51, UserPermission.IsShowRacing_153_51);

            return actionIdVisibilities;
        }

        private Dictionary<string, bool> GetSportBetTypeVisibilitiesFromUserPermission()
        {
            var sportBetTypeVisibilities = new Dictionary<string, bool>();
            sportBetTypeVisibilities.Add("151_31", UserPermission.IsShowRacing_151_31);
            sportBetTypeVisibilities.Add("151_41", UserPermission.IsShowRacing_151_41);
            sportBetTypeVisibilities.Add("151_51", UserPermission.IsShowRacing_151_51);
            sportBetTypeVisibilities.Add("152_41", UserPermission.IsShowRacing_152_41);
            sportBetTypeVisibilities.Add("152_51", UserPermission.IsShowRacing_152_51);
            sportBetTypeVisibilities.Add("153_51", UserPermission.IsShowRacing_153_51);
            sportBetTypeVisibilities.Add("161_90", UserPermission.IsShowNumberGroup);
            sportBetTypeVisibilities.Add(Sport.VirtualSports.ToString(), UserPermission.IsShowVirtualSports);
            sportBetTypeVisibilities.Add(Sport.GoldDeluxe.ToString(), UserPermission.IsShowGoldDeluxeSettingForCredit);
            sportBetTypeVisibilities.Add(Sport.ColossusBets.ToString(), UserPermission.IsShowColossusBets);
            sportBetTypeVisibilities.Add(Sport.Sportsbook2.ToString(), UserPermission.IsShowSportsbook2);
            sportBetTypeVisibilities.Add(Sport.CockFighting.ToString(), UserPermission.IsEnableProduct(Sport.CockFighting));
            sportBetTypeVisibilities.Add(Sport.AllBet.ToString(), UserPermission.IsEnableProduct(Sport.AllBet));
            sportBetTypeVisibilities.Add(Sport.Voidbridge.ToString(), UserPermission.IsEnableProduct(Sport.Voidbridge));

            return sportBetTypeVisibilities;
        }

        private void AppendActionIdVisibilities(Dictionary<int, bool> actionIdVisibilities, List<int> actionIds, bool visibility)
        {
            foreach (var actionId in actionIds)
            {
                if (!actionIdVisibilities.ContainsKey(actionId))
                {
                    actionIdVisibilities.Add(actionId, visibility);
                }
            }
        }

        private bool GetShowOldValueFromCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(CookieShowOldValue);

            bool showOldValue = cookie == null || Converter.ToInt(cookie.Value) == 1;

            return showOldValue;
        }
    }
}
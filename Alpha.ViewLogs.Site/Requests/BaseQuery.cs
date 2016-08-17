namespace Alpha.ViewLogs.Site.Requests
{
    using System;
    using System.Collections.Generic;
    using Configurations;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using ViewLogs.Constants;

    public class BaseQuery
    {
        private readonly DateTime maxReportDate;

        private readonly DateTime minReportDate;

        public BaseQuery()
        {
            ActionIdVisibilities = new Dictionary<int, bool>();
            SetActionIdVisibilities();
            SetSportBetTypeVisibilities();

            // Limit time range for view log in 2 years (start from beginning of last year)
            this.maxReportDate = DateTime.Today;
            this.minReportDate = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1);
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

        public Dictionary<int, bool> ActionIdVisibilities { get; set; }

        public Dictionary<string, bool> SportBetTypeVisibilities { get; set; }

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

        public void AssignServerSideParam(IUserSession currentUserSession, bool isShowOldValue)
        {
            RoleId = currentUserSession.RoleId;
            HasViewLogPermission = currentUserSession.SubPageAccess != null && currentUserSession.SubPageAccess.ViewLog;
            Language = currentUserSession.Language;
            IsInternal = false;
            IsShowOldValue = isShowOldValue;
            UplineId = currentUserSession.CustId;
            IsShowMyanmarOdds = UserPermission.IsShowMyanmarOdds;
        }

        private void SetActionIdVisibilities()
        {
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_151_31, UserPermission.IsShowRacing_151_31);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_151_41, UserPermission.IsShowRacing_151_41);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_151_51, UserPermission.IsShowRacing_151_51);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_152_41, UserPermission.IsShowRacing_152_41);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_152_51, UserPermission.IsShowRacing_152_51);
            AppendActionIdVisibilities(AppSettings.ActionIds_Racing_153_51, UserPermission.IsShowRacing_153_51);
        }

        private void SetSportBetTypeVisibilities()
        {
            SportBetTypeVisibilities = new Dictionary<string, bool>();
            SportBetTypeVisibilities.Add("151_31", UserPermission.IsShowRacing_151_31);
            SportBetTypeVisibilities.Add("151_41", UserPermission.IsShowRacing_151_41);
            SportBetTypeVisibilities.Add("151_51", UserPermission.IsShowRacing_151_51);
            SportBetTypeVisibilities.Add("152_41", UserPermission.IsShowRacing_152_41);
            SportBetTypeVisibilities.Add("152_51", UserPermission.IsShowRacing_152_51);
            SportBetTypeVisibilities.Add("153_51", UserPermission.IsShowRacing_153_51);
            SportBetTypeVisibilities.Add("161_90", UserPermission.IsShowNumberGroup);
            SportBetTypeVisibilities.Add(Sport.VirtualSports.ToString(), UserPermission.IsShowVirtualSports);
            SportBetTypeVisibilities.Add(Sport.GoldDeluxe.ToString(), UserPermission.IsShowGoldDeluxeSettingForCredit);
            SportBetTypeVisibilities.Add(Sport.ColossusBets.ToString(), UserPermission.IsShowColossusBets);
            SportBetTypeVisibilities.Add(Sport.Sportsbook2.ToString(), UserPermission.IsShowSportsbook2);
            SportBetTypeVisibilities.Add(Sport.CockFighting.ToString(), UserPermission.IsEnableProduct(Sport.CockFighting));
            SportBetTypeVisibilities.Add(Sport.AllBet.ToString(), UserPermission.IsEnableProduct(Sport.AllBet));
            SportBetTypeVisibilities.Add(Sport.Voidbridge.ToString(), UserPermission.IsEnableProduct(Sport.Voidbridge));
        }

        private void AppendActionIdVisibilities(List<int> actionIds, bool visibility)
        {
            foreach (var actionId in actionIds)
            {
                if (!ActionIdVisibilities.ContainsKey(actionId))
                {
                    ActionIdVisibilities.Add(actionId, visibility);
                }
            }
        }
    }
}
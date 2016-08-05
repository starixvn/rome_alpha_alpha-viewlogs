namespace Alpha.ViewLogs.Site.ViewModels.CustomerSettingLogs
{
    using Fanex.Common.Helper;
    using Models;
    using Models.Enumerations;
    using Params.CustomerSettingLogs;
    using System;
    using System.Collections.Generic;

    public class CustomerSettingLogsViewModel
    {
        private readonly byte roleId;
        private readonly bool hasViewLogPermission;
        private readonly bool isSubAccount;
        private readonly int totalRecords;

        private readonly string maxReportDate;
        private readonly string minReportDate;
        private readonly string fromDate;
        private readonly string toDate;
        private readonly string dayInMonth;

        public CustomerSettingLogsViewModel(
            GetLogsParam logsParam,
            PagingLogs pagingLogs)
        {
            if (logsParam == null || pagingLogs == null)
            {
                return;
            }

            Check.Argument.IsNotNull(logsParam, "GetLogsParam");
            Check.Argument.IsNotNull(pagingLogs, "PagingLogs");

            this.roleId = logsParam.RoleId;
            this.hasViewLogPermission = logsParam.HasViewLogPermission;
            this.isSubAccount = logsParam.SubAccId > 0;

            this.totalRecords = pagingLogs.TotalRecords;

            var compareDate = new DateTime(logsParam.MinReportDate.Year, logsParam.MinReportDate.Month, logsParam.FromDate.Day);
            var dateInRange = compareDate < logsParam.MinReportDate ? logsParam.MinReportDate : logsParam.FromDate;
            this.fromDate = dateInRange.Day.ToString();

            compareDate = new DateTime(logsParam.MaxReportDate.Year, logsParam.MaxReportDate.Month, logsParam.ToDate.Day);
            dateInRange = compareDate < logsParam.MaxReportDate ? logsParam.ToDate : logsParam.MaxReportDate;
            this.toDate = dateInRange.Day.ToString();

            this.dayInMonth = logsParam.MinReportDate < logsParam.FromDate ?
                logsParam.FromDate.ToShortDateString() :
                logsParam.MinReportDate.ToShortDateString();

            // Limit time range for view log in 2 years (start from beginning of last year)
            this.maxReportDate = logsParam.MaxReportDate.ToShortDateString();
            this.minReportDate = logsParam.MinReportDate.ToShortDateString();
        }

        public byte RoleId
        {
            get
            {
                return this.roleId;
            }
        }

        public string UserLevel
        {
            get
            {
                return Enumeration.FromValue<UserLevel>(this.roleId).DisplayName.ToLower();
            }
        }

        public bool HasViewLogPermission
        {
            get
            {
                return hasViewLogPermission;
            }
        }

        public bool IsSubAccount
        {
            get
            {
                return isSubAccount;
            }
        }

        public int TotalRecords
        {
            get
            {
                return totalRecords;
            }
        }

        public List<LogActionTypeViewModel> LogActionTypes { get; set; }

        public List<BaseLogViewModel> Logs { get; set; }

        public string SelectedTab { get; set; }

        public List<ElementViewModal> Tabs { get; set; }

        public string DayInMonth
        {
            get
            {
                return this.dayInMonth;
            }
        }

        public string FromDate
        {
            get
            {
                return this.fromDate;
            }
        }

        public string ToDate
        {
            get
            {
                return this.toDate;
            }
        }

        public string MinReportDate
        {
            get
            {
                return this.minReportDate;
            }
        }

        public string MaxReportDate
        {
            get
            {
                return this.maxReportDate;
            }
        }

        public string ShowOldValuesCookieName { get; set; }
    }
}
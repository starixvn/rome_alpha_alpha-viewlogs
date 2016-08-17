namespace Alpha.ViewLogs.Site.ViewModels.Status
{
    using Models;
    using Params.Status;
    using System;
    using System.Collections.Generic;

    public class GetLogsViewModel
    {
        private readonly bool showOldValue;
        private readonly int pageSize;
        private readonly int pageIndex;

        private readonly string maxReportDate;
        private readonly string minReportDate;
        private readonly string fromDate;
        private readonly string toDate;
        private readonly string dayInMonth;

        private readonly string username;

        public GetLogsViewModel(GetLogsParam logsParam, PagingLogs<BaseLog> pagingLogs)
        {
            this.showOldValue = logsParam.IsShowOldValue;
            this.TotalRecords = pagingLogs.TotalRecords;
            this.pageSize = logsParam.PageSize;
            this.pageIndex = logsParam.PageIndex;

            var compareDate = new DateTime(logsParam.FromDate.Year, logsParam.FromDate.Month, logsParam.FromDate.Day);
            var dateInRange = compareDate < logsParam.MinReportDate ? logsParam.MinReportDate : logsParam.FromDate;
            this.fromDate = dateInRange.Day.ToString();

            compareDate = new DateTime(logsParam.ToDate.Year, logsParam.ToDate.Month, logsParam.ToDate.Day);
            dateInRange = compareDate < logsParam.MaxReportDate ? logsParam.ToDate : logsParam.MaxReportDate;
            this.toDate = dateInRange.Day.ToString();

            this.dayInMonth = logsParam.MinReportDate < logsParam.FromDate ?
                logsParam.FromDate.ToShortDateString() :
                logsParam.MinReportDate.ToShortDateString();

            // Limit time range for view log in 2 years (start from beginning of last year)
            this.maxReportDate = logsParam.MaxReportDate.ToShortDateString();
            this.minReportDate = logsParam.MinReportDate.ToShortDateString();

            this.username = logsParam.Username;
        }

        public bool ShowOldValue
        {
            get
            {
                return showOldValue;
            }
        }

        public int TotalRecords { get; set; }

        public int PageSize
        {
            get
            {
                return pageSize;
            }
        }

        public int PageIndex
        {
            get
            {
                return pageIndex;
            }
        }

        public List<BaseViewModel> Logs { get; set; }

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

        public string Username
        {
            get
            {
                return username;
            }
        }
    }
}
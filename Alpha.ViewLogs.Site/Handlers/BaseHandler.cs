namespace Alpha.ViewLogs.Site.Handlers
{
    using System;
    using Alpha.ViewLogs.Params;
    using Fanex.Common;
    using Models;
    using Requests;
    using Services;
    using ViewLogs.Constants;
    using ViewModels;

    public class BaseHandler
    {
        protected readonly XsltLogFormatter xsltLogFormatter;
        protected readonly LogService logService;


        public BaseHandler(LogService logService, XsltLogFormatter xsltLogFormatter)
        {
            this.xsltLogFormatter = xsltLogFormatter;
            this.logService = logService;
        }

        public LogParam GetLogsParam(BaseQuery indexQuery)
        {
            var fromDate = string.IsNullOrEmpty(indexQuery.FromDate) ? DateTime.Now.AddDays(-1) : Converter.ToDateTime(indexQuery.FromDate);
            var toDate = string.IsNullOrEmpty(indexQuery.ToDate) ? DateTime.Now : Converter.ToDateTime(indexQuery.ToDate);

            return new LogParam
            {
                UplineId = indexQuery.UplineId,
                SubAccId = indexQuery.SubAccId,
                RoleId = indexQuery.RoleId,
                FromDate = fromDate,
                ToDate = toDate,
                Username = indexQuery.Username,
                IsInternal = indexQuery.IsInternal,

                PageSize = string.IsNullOrEmpty(indexQuery.PageSize) ? Pager.DefaultPageSize : Converter.ToInt(indexQuery.PageSize),
                PageIndex = string.IsNullOrEmpty(indexQuery.PageIndex) ? 0 : Converter.ToInt(indexQuery.PageIndex),

                Language = indexQuery.Language,
                HasViewLogPermission = indexQuery.HasViewLogPermission,
                ActionIdVisibilities = indexQuery.ActionIdVisibilities,

                MaxReportDate = indexQuery.MaxReportDate,
                MinReportDate = indexQuery.MinReportDate,
                IsShowOldValue = indexQuery.IsShowOldValue
            };
        }

        public void AssignIndexViewModelValue(LogParam logsParam, PagingLogs<Log> pagingLogs, BaseIndexViewModel indexViewModel)
        {
            var dateInRange = logsParam.FromDate < logsParam.MinReportDate ? logsParam.MinReportDate : logsParam.FromDate;
            var fromDate = dateInRange.Day.ToString();

            dateInRange = logsParam.ToDate < logsParam.MaxReportDate ? logsParam.ToDate : logsParam.MaxReportDate;
            var toDate = dateInRange.Day.ToString();

            indexViewModel.HasViewLogPermission = logsParam.HasViewLogPermission;
            indexViewModel.IsSubAccount = logsParam.SubAccId > 0;
            indexViewModel.RoleId = logsParam.RoleId;

            indexViewModel.ShowOldValue = logsParam.IsShowOldValue;
            indexViewModel.TotalRecords = pagingLogs.TotalRecords;
            indexViewModel.PageSize = logsParam.PageSize;
            indexViewModel.PageIndex = logsParam.PageIndex;

            indexViewModel.FromDate = fromDate;
            indexViewModel.ToDate = toDate;

            indexViewModel.DayInMonth = logsParam.MinReportDate < logsParam.FromDate ?
                logsParam.FromDate.ToShortDateString() :
                logsParam.MinReportDate.ToShortDateString();

            indexViewModel.MaxReportDate = logsParam.MaxReportDate.ToShortDateString();
            indexViewModel.MinReportDate = logsParam.MinReportDate.ToShortDateString();

            indexViewModel.Username = logsParam.Username;
        }
    }
}
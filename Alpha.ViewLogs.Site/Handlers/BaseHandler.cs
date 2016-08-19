namespace Alpha.ViewLogs.Site.Handlers
{
    using System;
    using Alpha.ViewLogs.Params;
    using Constants;
    using Fanex.Common;
    using Requests;

    public class BaseHandler
    {
        protected LogParam CreateLogParam(LogQuery logQuery)
        {
            var fromDate = string.IsNullOrEmpty(logQuery.FromDate)
                ? DateTime.Now.AddDays(-1)
                : Converter.ToDateTime(logQuery.FromDate);

            var toDate = string.IsNullOrEmpty(logQuery.ToDate)
                ? DateTime.Now
                : Converter.ToDateTime(logQuery.ToDate);

            return new LogParam
            {
                UplineId = logQuery.UplineId,
                SubAccId = logQuery.SubAccId,
                RoleId = logQuery.RoleId,
                FromDate = fromDate,
                ToDate = toDate,
                Username = logQuery.Username,
                IsInternal = logQuery.IsInternal,

                PageSize = string.IsNullOrEmpty(logQuery.PageSize)
                ? Pager.DefaultPageSize
                : Converter.ToInt(logQuery.PageSize),

                PageIndex = string.IsNullOrEmpty(logQuery.PageIndex)
                ? 0
                : Converter.ToInt(logQuery.PageIndex),

                Language = logQuery.Language,
                HasViewLogPermission = logQuery.HasViewLogPermission,
                ActionIdVisibilities = logQuery.ActionIdVisibilities,

                MaxReportDate = logQuery.MaxReportDate,
                MinReportDate = logQuery.MinReportDate,
                IsShowOldValue = logQuery.IsShowOldValue
            };
        }
    }
}
namespace Alpha.ViewLogs.Site.Handlers
{
    using System;
    using Alpha.ViewLogs.Params;
    using Fanex.Common;
    using Requests;
    using ViewLogs.Constants;

    public class BaseHandler
    {
        protected LogParam CreateLogParam(LogQuery logQuery)
        {
            var fromDate = string.IsNullOrWhiteSpace(logQuery.FromDate)
                ? DateTime.Now.AddDays(-1)
                : Converter.ToDateTime(logQuery.FromDate);

            var toDate = string.IsNullOrWhiteSpace(logQuery.ToDate)
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

                PageSize = string.IsNullOrWhiteSpace(logQuery.PageSize)
                ? DefaultVariables.PageSize
                : Converter.ToInt(logQuery.PageSize),

                PageIndex = string.IsNullOrWhiteSpace(logQuery.PageIndex)
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
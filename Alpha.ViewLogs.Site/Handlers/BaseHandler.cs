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
            var fromDate = GetFromDate(logQuery);
            var toDate = GetToDate(logQuery);

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

        protected DateTime GetFromDate(LogQuery logQuery)
        {
            if (logQuery == null || string.IsNullOrWhiteSpace(logQuery.FromDate))
            {
                return DateTime.Now.Day > 1
                    ? DateTime.Now.AddDays(-1)
                    : DateTime.Now;
            }

            return Converter.ToDateTime(logQuery.FromDate);
        }

        protected DateTime GetToDate(LogQuery logQuery)
        {
            return logQuery == null || string.IsNullOrWhiteSpace(logQuery.ToDate)
                ? DateTime.Now
                : Converter.ToDateTime(logQuery.ToDate);
        }
    }
}
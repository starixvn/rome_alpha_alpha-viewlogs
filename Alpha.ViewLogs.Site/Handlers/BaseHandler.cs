namespace Alpha.ViewLogs.Site.Handlers
{
    using System;
    using Alpha.ViewLogs.Params;
    using Fanex.Common;
    using Requests;
    using Services;
    using ViewLogs.Constants;

    public class BaseHandler
    {
        private readonly XsltLogFormatter xsltLogFormatter;

        public BaseHandler(XsltLogFormatter xsltLogFormatter)
        {
            this.xsltLogFormatter = xsltLogFormatter;
        }

        public XsltLogFormatter XsltLogFormatter
        {
            get
            {
                return xsltLogFormatter;
            }
        }

        public GetLogsParam GetLogsParam(BaseQuery indexQuery)
        {
            var fromDate = string.IsNullOrEmpty(indexQuery.FromDate) ? DateTime.Now.AddDays(-1) : Converter.ToDateTime(indexQuery.FromDate);
            var toDate = string.IsNullOrEmpty(indexQuery.ToDate) ? DateTime.Now : Converter.ToDateTime(indexQuery.ToDate);

            return new GetLogsParam
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
    }
}
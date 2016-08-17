namespace Alpha.ViewLogs.Site.Requests.Login
{
    using System;
    using Fanex.Common;
    using MediatR;
    using Params;
    using ViewLogs.Constants;
    using ViewModels.Login;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        public BaseGetLogsParam ConvertToGetLogsParam()
        {
            var fromDate = string.IsNullOrEmpty(FromDate) ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(FromDate);
            var toDate = string.IsNullOrEmpty(ToDate) ? DateTime.Now : Convert.ToDateTime(ToDate);

            return new BaseGetLogsParam
            {
                UplineId = UplineId,
                SubAccId = SubAccId,
                RoleId = RoleId,
                FromDate = fromDate,
                ToDate = toDate,
                Username = Username,
                IsInternal = IsInternal,

                PageSize = string.IsNullOrEmpty(PageSize) ? Pager.DefaultPageSize : Converter.ToInt(PageSize),
                PageIndex = string.IsNullOrEmpty(PageIndex) ? 0 : Converter.ToInt(PageIndex),

                Language = Language,
                HasViewLogPermission = HasViewLogPermission,
                ActionIdVisibilities = ActionIdVisibilities,

                MaxReportDate = MaxReportDate,
                MinReportDate = MinReportDate,
                IsShowOldValue = IsShowOldValue
            };
        }
    }
}
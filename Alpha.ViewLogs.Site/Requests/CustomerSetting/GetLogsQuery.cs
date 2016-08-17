namespace Alpha.ViewLogs.Site.Requests.CustomerSetting
{
    using System;
    using Fanex.Common;
    using MediatR;
    using Params;
    using ViewLogs.Constants;
    using ViewModels.CustomerSetting;

    public class GetLogsQuery : BaseQuery, IRequest<GetLogsViewModel>
    {
        public bool IsDebug { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }

        public CustomerSettingParam ConvertToGetLogsParam()
        {
            var fromDate = string.IsNullOrEmpty(FromDate) ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(FromDate);
            var toDate = string.IsNullOrEmpty(ToDate) ? DateTime.Now : Convert.ToDateTime(ToDate);

            return new CustomerSettingParam
            {
                UplineId = UplineId,
                IsDebug = IsDebug,
                SubAccId = SubAccId,
                ActionTypeId = string.IsNullOrEmpty(ActionTypeId) ? ActionType.All : Converter.ToInt(ActionTypeId),
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
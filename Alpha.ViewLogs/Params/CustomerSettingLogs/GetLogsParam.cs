namespace Alpha.ViewLogs.Params.CustomerSettingLogs
{
    using Criteria.CustomerSettingLogs;
    using System;
    using System.Collections.Generic;

    public class GetLogsParam
    {
        public int UplineId { get; set; }

        public bool IsDebug { get; set; }

        public int SubAccId { get; set; }

        public int ActionTypeId { get; set; }

        public byte RoleId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Username { get; set; }

        public bool HasViewLogPermission { get; set; }

        public bool IsInternal { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string Language { get; set; }

        public bool IsShowOldValue { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }

        public Dictionary<int, bool> ActionIdVisibilities { get; set; }

        public DateTime MaxReportDate { get; set; }

        public DateTime MinReportDate { get; set; }

        public GetCustomerSettingLogsCriteria CreateGetLogsCriteria(int custId)
        {
            return new GetCustomerSettingLogsCriteria
            {
                CustId = custId,
                CustName = Username,
                FromDate = FromDate,
                ToDate = ToDate,
                ActionTypeId = ActionTypeId,
                IsInternal = IsInternal,
                PageSize = PageSize,
                PageIndex = PageIndex
            };
        }

        public GetTotalRecordsOfCustomerSettingLogsCriteria CreateTotalRecordsOfLogsCriteria(int custId)
        {
            return new GetTotalRecordsOfCustomerSettingLogsCriteria
            {
                CustId = custId,
                FromDate = FromDate,
                ToDate = ToDate,
                ActionTypeId = ActionTypeId,
                IsInternal = IsInternal
            };
        }
    }
}

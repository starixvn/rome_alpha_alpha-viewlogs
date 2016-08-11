namespace Alpha.ViewLogs.Params.LoginLogs
{
    using Criteria.LoginLogs;
    using System;
    using System.Collections.Generic;

    public class GetLogsParam
    {
        public int UplineId { get; set; }

        public int SubAccId { get; set; }

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

        public Dictionary<int, bool> ActionIdVisibilities { get; set; }

        public DateTime MaxReportDate { get; set; }

        public DateTime MinReportDate { get; set; }

        public GetLoginLogsCriteria CreateGetLogsCriteria(int custId)
        {
            return new GetLoginLogsCriteria
            {
                CustId = custId,
                CustName = Username,
                FromDate = FromDate,
                ToDate = ToDate,
                IsInternal = IsInternal,
                PageSize = PageSize,
                PageIndex = PageIndex
            };
        }

        public GetTotalRecordsOfLoginLogsCriteria CreateTotalRecordsOfLogsCriteria(int custId)
        {
            return new GetTotalRecordsOfLoginLogsCriteria
            {
                CustId = custId,
                FromDate = FromDate,
                ToDate = ToDate,
                IsInternal = IsInternal
            };
        }
    }
}

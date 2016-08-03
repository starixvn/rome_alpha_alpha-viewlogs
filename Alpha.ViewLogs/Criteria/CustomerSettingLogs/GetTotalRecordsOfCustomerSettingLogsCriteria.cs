namespace Alpha.ViewLogs.Criteria.CustomerSettingLogs
{
    using Fanex.Data.Repository;
    using System;

    public class GetTotalRecordsOfCustomerSettingLogsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int ActionTypeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int? LowerActionId { get; set; }

        public int? UpperActionId { get; set; }

        public bool IsInternal { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_RunSPSelTotalRecords";
        }
    }
}

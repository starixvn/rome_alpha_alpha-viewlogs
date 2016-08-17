namespace Alpha.ViewLogs.Criteria.CustomerSetting
{
    using Fanex.Data.Repository;
    using System;

    public class GetTotalRecordsOfCustomerSettingLogsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int ActionTypeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsInternal { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_RunSPSelTotalRecords";
        }
    }
}

namespace Alpha.ViewLogs.Criteria.CustomerSetting
{
    using System;
    using Fanex.Data.Repository;

    public class GetTotalCustomerSettingLogsCriteria : CriteriaBase
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

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId >= 0;
        }
    }
}

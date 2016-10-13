namespace Alpha.ViewLogs.Criteria.Setting
{
    using System;
    using Fanex.Data.Repository;

    public class GetTotalSettingLogsCriteria : CriteriaBase
    {
        public GetTotalSettingLogsCriteria(
            int custId,
            int actionTypeId,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal)
        {
            CustId = custId;
            ActionTypeId = actionTypeId;
            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
        }

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
            return FromDate <= ToDate && CustId > 0;
        }
    }
}

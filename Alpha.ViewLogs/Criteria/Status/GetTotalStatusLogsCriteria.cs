namespace Alpha.ViewLogs.Criteria.Status
{
    using System;
    using Fanex.Data.Repository;

    public class GetTotalStatusLogsCriteria : CriteriaBase
    {
        public GetTotalStatusLogsCriteria(
            int custId,
            int lowerActionId,
            int upperActionId,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal)
        {
            CustId = custId;
            LowerActionId = lowerActionId;
            UpperActionId = upperActionId;

            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
        }

        public int CustId { get; set; }

        public int LowerActionId { get; set; }

        public int UpperActionId { get; set; }

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

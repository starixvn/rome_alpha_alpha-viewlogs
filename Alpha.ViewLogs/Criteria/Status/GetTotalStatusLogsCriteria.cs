namespace Alpha.ViewLogs.Criteria.Status
{
    using System;
    using Fanex.Data.Repository;

    public class GetTotalStatusLogsCriteria : CriteriaBase
    {
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
    }
}

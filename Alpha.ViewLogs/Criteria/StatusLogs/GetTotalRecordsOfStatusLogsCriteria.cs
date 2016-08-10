namespace Alpha.ViewLogs.Criteria.StatusLogs
{
    using Fanex.Data.Repository;
    using System;

    public class GetTotalRecordsOfStatusLogsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

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

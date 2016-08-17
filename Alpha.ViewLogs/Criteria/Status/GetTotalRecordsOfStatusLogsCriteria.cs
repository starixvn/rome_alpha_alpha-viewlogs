namespace Alpha.ViewLogs.Criteria.Status
{
    using System;
    using Fanex.Data.Repository;

    public class GetTotalRecordsOfStatusLogsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsInternal { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_RunSPSelTotalRecords";
        }
    }
}

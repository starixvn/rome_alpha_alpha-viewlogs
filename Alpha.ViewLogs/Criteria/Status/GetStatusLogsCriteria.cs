namespace Alpha.ViewLogs.Criteria.Status
{
    using System;
    using Constants;
    using Fanex.Data.Repository;

    public class GetStatusLogsCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public string CustName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsInternal { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_RunSPSel";
        }
    }
}

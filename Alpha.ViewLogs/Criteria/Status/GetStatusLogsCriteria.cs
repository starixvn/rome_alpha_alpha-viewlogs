namespace Alpha.ViewLogs.Criteria.Status
{
    using System;
    using Fanex.Data.Repository;

    public class GetStatusLogsCriteria : CriteriaBase
    {
        public GetStatusLogsCriteria(
            int custId,
            string custName,
            int lowerActionId,
            int upperActionId,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal,
            int pageIndex,
            int pageSize)
        {
            CustId = custId;
            CustName = custName;
            LowerActionId = lowerActionId;
            UpperActionId = upperActionId;

            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public int LowerActionId { get; set; }

        public int UpperActionId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsInternal { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_RunSPSel";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId > 0;
        }
    }
}

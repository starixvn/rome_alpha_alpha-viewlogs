namespace Alpha.ViewLogs.Criteria.Credit
{
    using System;
    using Fanex.Data.Repository;

    public class GetCreditLogsCriteria : CriteriaBase
    {
        public GetCreditLogsCriteria(
            int custId,
            string custName,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal,
            int pageIndex,
            int pageSize)
        {
            CustId = custId;
            CustName = custName;
            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int CustId { get; private set; }

        public string CustName { get; private set; }

        public DateTime FromDate { get; private set; }

        public DateTime ToDate { get; private set; }

        public bool IsInternal { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_CreditSel";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId >= 0;
        }
    }
}

namespace Alpha.ViewLogs.Criteria.Setting
{
    using System;
    using Fanex.Data.Repository;

    public class GetSettingLogsCriteria : CriteriaBase
    {
        public GetSettingLogsCriteria(
            int custId,
            string custName,
            int actionTypeId,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal,
            int pageIndex,
            int pageSize)
        {
            CustId = custId;
            CustName = custName;
            ActionTypeId = actionTypeId;
            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public int CustId { get; private set; }

        public string CustName { get; private set; }

        public int ActionTypeId { get; private set; }

        public DateTime FromDate { get; private set; }

        public DateTime ToDate { get; private set; }

        public bool IsInternal { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_RunSPSel";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId >= 0 && !string.IsNullOrEmpty(CustName);
        }
    }
}

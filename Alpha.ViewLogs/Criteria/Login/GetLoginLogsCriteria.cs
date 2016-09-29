namespace Alpha.ViewLogs.Criteria.Login
{
    using System;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetLoginLogsCriteria : CriteriaBase
    {
        public GetLoginLogsCriteria(
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
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        [SpParam(SqlParamName = "UserId")]
        public int CustId { get; private set; }

        [SpParam(SqlParamName = "UserName")]
        public string CustName { get; private set; }

        public DateTime FromDate { get; private set; }

        public DateTime ToDate { get; private set; }

        [SpParam(SqlParamName = "IsAdminAccess")]
        public bool IsInternal { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_LoginSel";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId >= 0;
        }
    }
}

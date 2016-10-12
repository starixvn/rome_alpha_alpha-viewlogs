namespace Alpha.ViewLogs.Criteria.Login
{
    using System;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetTotalLoginLogsCriteria : CriteriaBase
    {
        public GetTotalLoginLogsCriteria(
            int custId,
            DateTime fromDate,
            DateTime toDate,
            bool isInternal)
        {
            CustId = custId;
            FromDate = fromDate;
            ToDate = toDate;
            IsInternal = isInternal;
        }

        [SpParam(SqlParamName = "UserId")]
        public int CustId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        [SpParam(SqlParamName = "IsAdminAccess")]
        public bool IsInternal { get; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_LoginSelTotalRecords";
        }

        public override bool IsValid()
        {
            return FromDate <= ToDate && CustId >= 0;
        }
    }
}

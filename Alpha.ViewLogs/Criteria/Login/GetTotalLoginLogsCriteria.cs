namespace Alpha.ViewLogs.Criteria.Login
{
    using System;
    using Fanex.Data.Repository;

    public class GetTotalLoginLogsCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "UserId")]
        public int CustId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [SpParam(SqlParamName = "IsAdminAccess")]
        public bool IsInternal { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_LoginSelTotalRecords";
        }
    }
}

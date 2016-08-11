namespace Alpha.ViewLogs.Criteria.LoginLogs
{
    using Fanex.Data.Repository;
    using System;

    public class GetTotalRecordsOfLoginLogsCriteria : CriteriaBase
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

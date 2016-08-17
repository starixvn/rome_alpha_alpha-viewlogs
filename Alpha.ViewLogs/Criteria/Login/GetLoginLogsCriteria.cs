namespace Alpha.ViewLogs.Criteria.Login
{
    using System;
    using Fanex.Data.Repository;

    public class GetLoginLogsCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "UserId")]
        public int CustId { get; set; }

        [SpParam(SqlParamName = "UserName")]
        public string CustName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [SpParam(SqlParamName = "IsAdminAccess")]
        public bool IsInternal { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UserLog_LoginSel";
        }
    }
}

namespace Alpha.ViewLogs.Params.Login
{
    using Criteria.Login;

    public class GetLogsParam : GetBaseLogsParam
    {
        public GetLoginLogsCriteria CreateGetLogsCriteria(int custId)
        {
            return new GetLoginLogsCriteria
            {
                CustId = custId,
                CustName = Username,
                FromDate = FromDate,
                ToDate = ToDate,
                IsInternal = IsInternal,
                PageSize = PageSize,
                PageIndex = PageIndex
            };
        }

        public GetTotalRecordsOfLoginLogsCriteria CreateTotalRecordsOfLogsCriteria(int custId)
        {
            return new GetTotalRecordsOfLoginLogsCriteria
            {
                CustId = custId,
                FromDate = FromDate,
                ToDate = ToDate,
                IsInternal = IsInternal
            };
        }
    }
}

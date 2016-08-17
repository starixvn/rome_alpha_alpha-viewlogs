namespace Alpha.ViewLogs.Params.Credit
{
    using Criteria.Credit;

    public class GetLogsParam : GetBaseLogsParam
    {
        public GetCreditLogsCriteria CreateGetLogsCriteria(int custId)
        {
            return new GetCreditLogsCriteria
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

        public GetTotalRecordsOfCreditLogsCriteria CreateTotalRecordsOfLogsCriteria(int custId)
        {
            return new GetTotalRecordsOfCreditLogsCriteria
            {
                CustId = custId,
                FromDate = FromDate,
                ToDate = ToDate,
                IsInternal = IsInternal
            };
        }
    }
}

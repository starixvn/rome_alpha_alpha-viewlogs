namespace Alpha.ViewLogs.Params.Status
{
    using Criteria.Status;

    public class GetLogsParam : GetBaseLogsParam
    {
        public bool IsDebug { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }

        public GetStatusLogsCriteria CreateGetLogsCriteria(int custId)
        {
            return new GetStatusLogsCriteria
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

        public GetTotalRecordsOfStatusLogsCriteria CreateTotalRecordsOfLogsCriteria(int custId)
        {
            return new GetTotalRecordsOfStatusLogsCriteria
            {
                CustId = custId,
                FromDate = FromDate,
                ToDate = ToDate,
                IsInternal = IsInternal
            };
        }
    }
}

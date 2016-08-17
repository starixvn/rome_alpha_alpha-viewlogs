namespace Alpha.ViewLogs.Params.CustomerSetting
{
    using Criteria.CustomerSetting;

    public class GetLogsParam : GetBaseLogsParam
    {
        public bool IsDebug { get; set; }

        public int ActionTypeId { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }

        public GetCustomerSettingLogsCriteria CreateGetLogsCriteria(int custId)
        {
            return new GetCustomerSettingLogsCriteria
            {
                CustId = custId,
                CustName = Username,
                FromDate = FromDate,
                ToDate = ToDate,
                ActionTypeId = ActionTypeId,
                IsInternal = IsInternal,
                PageSize = PageSize,
                PageIndex = PageIndex
            };
        }

        public GetTotalRecordsOfCustomerSettingLogsCriteria CreateTotalRecordsOfLogsCriteria(int custId)
        {
            return new GetTotalRecordsOfCustomerSettingLogsCriteria
            {
                CustId = custId,
                FromDate = FromDate,
                ToDate = ToDate,
                ActionTypeId = ActionTypeId,
                IsInternal = IsInternal
            };
        }
    }
}

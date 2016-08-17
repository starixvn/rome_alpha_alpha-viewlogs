namespace Alpha.ViewLogs.Site.ViewModels.CustomerSetting
{
    using Models;
    using Params;

    public class GetCustomerSettingLogsViewModel : GetLogsViewModel
    {
        private readonly int actionTypeId;

        public GetCustomerSettingLogsViewModel(CustomerSettingParam logsParam, PagingLogs<Log> pagingLogs) :
            base(logsParam, pagingLogs)
        {
            this.actionTypeId = logsParam.ActionTypeId;
        }

        public int ActionTypeId
        {
            get
            {
                return actionTypeId;
            }
        }
    }
}
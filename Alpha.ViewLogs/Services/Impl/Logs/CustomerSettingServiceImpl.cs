namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using Criteria.CustomerSetting;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;

    public class CustomerSettingServiceImpl : BaseService, LogService
    {
        public CustomerSettingServiceImpl(
             CachingManager cacheManager,
             IDynamicRepository dynamicRepository,
             CustomerService customerService,
             LogDataAccess baseDataAccess) :
            base(cacheManager, dynamicRepository, customerService, baseDataAccess)
        {
        }

        public override ICriteria CreateGetLogsCriteria(LogParam getLogsParam, int custId)
        {
            return new GetCustomerSettingLogsCriteria
            {
                CustId = custId,
                CustName = getLogsParam.Username,
                FromDate = DateTimeHelper.GetBeginningOfDate(getLogsParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(getLogsParam.ToDate),
                IsInternal = getLogsParam.IsInternal,
                PageSize = getLogsParam.PageSize,
                PageIndex = getLogsParam.PageIndex
            };
        }

        public override ICriteria CreateTotalRecordsOfLogsCriteria(LogParam getLogsParam, int custId)
        {
            return new GetTotalRecordsOfCustomerSettingLogsCriteria
            {
                CustId = custId,
                FromDate = DateTimeHelper.GetBeginningOfDate(getLogsParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(getLogsParam.ToDate),
                IsInternal = getLogsParam.IsInternal
            };
        }
    }
}

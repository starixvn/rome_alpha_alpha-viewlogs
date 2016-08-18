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

        protected override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            return new GetCustomerSettingLogsCriteria
            {
                CustId = logParam.CustId,
                CustName = logParam.Username,
                FromDate = DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                IsInternal = logParam.IsInternal,
                PageSize = logParam.PageSize,
                PageIndex = logParam.PageIndex
            };
        }

        protected override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            return new GetTotalCustomerSettingLogsCriteria
            {
                CustId = logParam.CustId,
                FromDate = DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                IsInternal = logParam.IsInternal
            };
        }
    }
}

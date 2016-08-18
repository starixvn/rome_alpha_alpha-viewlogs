namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria.Status;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;

    public class StatusServiceImpl : BaseService, StatusService
    {
        public StatusServiceImpl(
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess baseDataAccess) :
            base(cacheManager, dynamicRepository, customerService, baseDataAccess)
        {
        }

        public override ICriteria CreateGetLogsCriteria(LogParam getLogsParam, int custId)
        {
            return new GetStatusLogsCriteria
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
            return new GetTotalRecordsOfStatusLogsCriteria
            {
                CustId = custId,
                FromDate = DateTimeHelper.GetBeginningOfDate(getLogsParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(getLogsParam.ToDate),
                IsInternal = getLogsParam.IsInternal
            };
        }
    }
}

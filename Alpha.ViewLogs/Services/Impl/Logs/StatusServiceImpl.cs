namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using Criteria.Status;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;

    public class StatusServiceImpl : BaseService, LogService
    {
        private const int ActionIdFrom = 1100;
        private const int ActionIdTo = 1199;

        public StatusServiceImpl(
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess baseDataAccess) :
            base(cacheManager, dynamicRepository, customerService, baseDataAccess)
        {
        }

        protected override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            return new GetStatusLogsCriteria
            {
                //// Just get status from action id 1100 => 1199
                LowerActionId = ActionIdFrom,
                UpperActionId = ActionIdTo,
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
            return new GetTotalStatusLogsCriteria
            {
                LowerActionId = ActionIdFrom,
                UpperActionId = ActionIdTo,
                CustId = logParam.CustId,
                FromDate = DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                IsInternal = logParam.IsInternal
            };
        }
    }
}

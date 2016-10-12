namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
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
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess baseDataAccess) :
            base(dynamicRepository, customerService, baseDataAccess)
        {
        }

        internal override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetStatusLogsCriteria(
                logParam.CustId,
                logParam.Username,
                ActionIdFrom, // Just get status from action id 1100 , because it uses same SP with customer setting log
                ActionIdTo, // Just get status to action id 1199, because it uses same SP with customer setting log
                DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal,
                logParam.PageIndex,
                logParam.PageSize);
        }

        internal override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetTotalStatusLogsCriteria(
                logParam.CustId,
                //// Just get status from action id 1100 => 1199, because it uses same SP with customer setting log
                ActionIdFrom,
                ActionIdTo,
                DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal);
        }
    }
}

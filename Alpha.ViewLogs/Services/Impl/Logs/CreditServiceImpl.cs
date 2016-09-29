namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
    using Criteria.Credit;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;

    public class CreditServiceImpl : BaseService, LogService
    {
        public CreditServiceImpl(
             IDynamicRepository dynamicRepository,
             CustomerService customerService,
             LogDataAccess baseDataAccess) :
            base(dynamicRepository, customerService, baseDataAccess)
        {
        }

        protected override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetCreditLogsCriteria(
                logParam.CustId,
                logParam.Username,
                DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal,
                logParam.PageIndex,
                logParam.PageSize);
        }

        protected override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetTotalCreditLogsCriteria(
                logParam.CustId,
                DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal);
        }
    }
}

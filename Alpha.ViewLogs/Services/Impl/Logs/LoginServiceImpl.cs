namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System.Collections.Generic;
    using Criteria.Login;
    using DataAccesses;
    using DataAccesses.Impl;
    using Fanex.Data.Repository;
    using Helpers;
    using Models;
    using Params;

    public class LoginServiceImpl : BaseService, LogService
    {
        public LoginServiceImpl(
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess logDataAccess) :
            base(
                cacheManager, 
                dynamicRepository, 
                customerService, 
                logDataAccess)
        {
        }

        protected override IEnumerable<Log> GetLogsFromDataAccess(LogParam logParam)
        {
            var criteria = CreateGetLogsCriteria(logParam);

            return this.logDataAccess.GetLogs(criteria);
        }

        protected override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            return new GetLoginLogsCriteria
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
            return new GetTotalLoginLogsCriteria
            {
                CustId = logParam.CustId,
                FromDate = DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                IsInternal = logParam.IsInternal
            };
        }
    }
}

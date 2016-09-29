namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
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
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess logDataAccess) :
            base(dynamicRepository, customerService, logDataAccess)
        {
        }

        protected override IEnumerable<Log> GetLogs(LogParam logParam)
        {
            var criteria = CreateGetLogsCriteria(logParam);

            return this.LogDataAccess.GetLogs(criteria);
        }

        protected override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetLoginLogsCriteria(
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

            return new GetTotalLoginLogsCriteria(
                logParam.CustId,
                DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal);
        }
    }
}

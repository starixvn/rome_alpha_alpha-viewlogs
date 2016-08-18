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
                logDataAccess is LoginDataAccessImpl ? logDataAccess : new LoginDataAccessImpl(dynamicRepository))
        {
        }

        public override List<Log> GetLogsFromDataAccess(LogParam getLogsParam, CustomerInfo customerInfo)
        {
            var getLogsCriteria = CreateGetLogsCriteria(getLogsParam, customerInfo.CustId);

            return this.logDataAccess.GetLogs(getLogsCriteria);
        }

        public override ICriteria CreateGetLogsCriteria(LogParam getLogsParam, int custId)
        {
            return new GetLoginLogsCriteria
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
            return new GetTotalRecordsOfLoginLogsCriteria
            {
                CustId = custId,
                FromDate = DateTimeHelper.GetBeginningOfDate(getLogsParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(getLogsParam.ToDate),
                IsInternal = getLogsParam.IsInternal
            };
        }
    }
}

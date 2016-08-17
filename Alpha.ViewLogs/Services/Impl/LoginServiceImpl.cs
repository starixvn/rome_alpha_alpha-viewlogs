namespace Alpha.ViewLogs.Services.Impl
{
    using System.Collections.Generic;
    using Criteria.Login;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Models;
    using Params;

    public class LoginServiceImpl : BaseService, LoginService
    {
        private LoginDataAccess loginDataAccess;

        public LoginServiceImpl(
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            BaseDataAccess baseDataAccess,
            LoginDataAccess loginDataAccess) :
            base(cacheManager, dynamicRepository, customerService, baseDataAccess)
        {
            this.loginDataAccess = loginDataAccess;
        }

        public override List<Log> GetLogsFromDataAccess(GetLogsParam getLogsParam, CustomerInfo customerInfo)
        {
            var getLogsCriteria = CreateGetLogsCriteria(getLogsParam, customerInfo.CustId);

            return loginDataAccess.GetLogs(getLogsCriteria);
        }

        public override ICriteria CreateGetLogsCriteria(GetLogsParam getLogsParam, int custId)
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

        public override ICriteria CreateTotalRecordsOfLogsCriteria(GetLogsParam getLogsParam, int custId)
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

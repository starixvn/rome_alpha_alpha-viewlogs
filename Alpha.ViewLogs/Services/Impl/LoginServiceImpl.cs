namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using DataAccesses;
    using Models;
    using Models.Login;
    using Params.Login;
    using System.Collections.Generic;

    public class LoginServiceImpl : LoginService
    {
        private readonly LoginDataAccess loginLogsDataAccess;
        private readonly BaseService baseLogService;
        private readonly CustomerService customerService;
        private readonly BaseDataAccess baseDataAccess;

        public LoginServiceImpl(
            CustomerService customerService,
            LoginDataAccess loginLogsDataAccess,
            BaseService baseLogService,
            BaseDataAccess baseDataAccess)
        {
            this.loginLogsDataAccess = loginLogsDataAccess;
            this.baseLogService = baseLogService;
            this.baseDataAccess = baseDataAccess;
            this.customerService = customerService;
        }

        public PagingLogs<LoginLog> GetLogs(GetLogsParam getLogsParam)
        {
            var pagingLogs = new PagingLogs<LoginLog>
            {
                Logs = new List<LoginLog>()
            };

            var customerInfo = customerService.CheckIsBelongingByUsername(new CheckIsBelongingByUsernameCriteria
            {
                Username = getLogsParam.Username,
                UplineId = getLogsParam.UplineId
            });

            var isValidDownline = customerInfo != null && customerInfo.CustId > 0;
            if (!isValidDownline)
            {
                return pagingLogs;
            }

            var getLogsCriteria = getLogsParam.CreateGetLogsCriteria(customerInfo.CustId);

            pagingLogs.Logs = loginLogsDataAccess.GetLogs(getLogsCriteria);

            var getTotalRecordsOfLogsCriteria = getLogsParam.CreateTotalRecordsOfLogsCriteria(customerInfo.CustId);
            pagingLogs.TotalRecords = baseDataAccess.GetTotalRecordsOfLogs(getTotalRecordsOfLogsCriteria);

            foreach (var log in pagingLogs.Logs)
            {
                log.ActionName = baseLogService.GetLogActionName(log.ActionId, getLogsParam.Language);
            }

            return pagingLogs;
        }
    }
}

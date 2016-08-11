namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using DataAccesses;
    using Models.LoginLogs;
    using Params.LoginLogs;
    using System.Collections.Generic;

    public class LoginLogsServiceImpl : LoginLogsService
    {
        private readonly CustomerService customerService;
        private readonly BaseDataAccess baseDataAccess;
        private readonly LoginLogsDataAccess loginLogsDataAccess;
        private readonly BaseLogService baseLogService;

        public LoginLogsServiceImpl(
            CustomerService customerService,
            BaseDataAccess baseDataAccess,
            LoginLogsDataAccess loginLogsDataAccess,
            BaseLogService baseLogService)
        {
            this.customerService = customerService;
            this.baseDataAccess = baseDataAccess;
            this.loginLogsDataAccess = loginLogsDataAccess;
            this.baseLogService = baseLogService;
        }

        public PagingLoginLogs GetLogs(GetLogsParam getLogsParam)
        {
            var pagingLogs = new PagingLoginLogs
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

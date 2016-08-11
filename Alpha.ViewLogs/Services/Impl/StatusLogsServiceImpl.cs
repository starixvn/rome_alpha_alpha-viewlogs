namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;
    using Params.StatusLogs;
    using System.Collections.Generic;
    using System.Linq;

    public class StatusLogsServiceImpl : StatusLogsService
    {
        private readonly CustomerService customerService;
        private readonly BaseDataAccess baseDataAccess;
        private readonly IDynamicRepository dynamicRepository;
        private readonly BaseLogService baseLogService;

        public StatusLogsServiceImpl(
          CustomerService customerService,
          BaseDataAccess baseDataAccess,
          IDynamicRepository dynamicRepository,
          BaseLogService baseLogService)
        {
            this.customerService = customerService;
            this.baseDataAccess = baseDataAccess;
            this.dynamicRepository = dynamicRepository;
            this.baseLogService = baseLogService;
        }

        public PagingLogs GetLogs(GetLogsParam getLogsParam)
        {
            var pagingLogs = new PagingLogs
            {
                Logs = new List<BaseLog>()
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

            pagingLogs.Logs = dynamicRepository.Fetch<BaseLog>(getLogsCriteria).ToList();

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

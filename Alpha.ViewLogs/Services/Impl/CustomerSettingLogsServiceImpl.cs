namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using Criteria.CustomerSettingLogs;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;
    using Models.CustomerSettingLogs;
    using Params.CustomerSettingLogs;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerSettingLogsServiceImpl : CustomerSettingLogsService
    {
        private const string CacheKeyLogActionTypes = "Alpha.ViewLogs.CustomerSetting.LogActionTypes";

        private readonly CustomerService customerService;
        private readonly BaseDataAccess baseDataAccess;
        private readonly CachingManager cacheManager;
        private readonly IDynamicRepository dynamicRepository;
        private readonly BaseLogService baseLogService;

        public CustomerSettingLogsServiceImpl(
            CustomerService customerService,
            BaseDataAccess baseDataAccess,
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository,
            BaseLogService baseLogService)
        {
            this.customerService = customerService;
            this.baseDataAccess = baseDataAccess;
            this.cacheManager = cacheManager;
            this.dynamicRepository = dynamicRepository;
            this.baseLogService = baseLogService;
        }

        public List<LogActionType> GetLogActionTypes()
        {
            var logActionTypes = cacheManager.Get<List<LogActionType>>(CacheKeyLogActionTypes);
            if (logActionTypes != null && logActionTypes.Any())
            {
                return logActionTypes;
            }

            logActionTypes = dynamicRepository.Fetch<LogActionType>(new GetAllLogActionTypeCriteria()).ToList();
            cacheManager.Add(CacheKeyLogActionTypes, logActionTypes);

            return logActionTypes;
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

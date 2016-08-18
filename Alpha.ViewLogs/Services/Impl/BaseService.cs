namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;
    using Params;

    public abstract class BaseService
    {
        public const string CacheKeyLogAction = "Alpha.ViewLogs.CustomerSetting.LogAction";
        private const string CacheKeyLogActionTypes = "Alpha.ViewLogs.CustomerSetting.LogActionTypes";
        private const int HourToCacheLogActions = 1;

        private readonly CachingManager cacheManager;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerService customerService;
        private readonly BaseDataAccess baseDataAccess;

        protected BaseService(
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            BaseDataAccess baseDataAccess)
        {
            this.cacheManager = cacheManager;
            this.dynamicRepository = dynamicRepository;
            this.customerService = customerService;
            this.baseDataAccess = baseDataAccess;
        }

        public string GetLogActionName(int actionId, string language)
        {
            var logAction = GetLogActionByActionId(actionId);
            switch (language)
            {
                case Language.English:
                    return logAction.EnglishName;
                case Language.ChineseTraditional:
                    return logAction.ChineseTraditionalName;
                case Language.ChineseSimplified:
                    return logAction.ChineseSimplifiedName;
                case Language.Japanese:
                    return logAction.JapaneseName;
                case Language.Thai:
                    return logAction.ThaiName;
                case Language.Korean:
                    return logAction.KoreanName;
                case Language.Vietnamese:
                    return logAction.VietnameseName;
                default:
                    return logAction.EnglishName;
            }
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

        public LogAction GetLogActionByActionId(int actionId)
        {
            var logActionDictionary = cacheManager.Get<Dictionary<int, LogAction>>(CacheKeyLogAction);

            if (logActionDictionary == null || !logActionDictionary.Any())
            {
                logActionDictionary = new Dictionary<int, LogAction>();
                var logActions = dynamicRepository.Fetch<LogAction>(new GetLogActionsCriteria()).ToList();
                logActions.ForEach(logAction => logActionDictionary.Add(logAction.Id, logAction));
                cacheManager.Add(CacheKeyLogAction, logActionDictionary, TimeSpan.FromHours(HourToCacheLogActions));
            }

            return logActionDictionary[actionId];
        }

        public PagingLogs<Log> GetLogs(LogParam getLogsParam)
        {
            var pagingLogs = new PagingLogs<Log>
            {
                Logs = new List<Log>()
            };

            var customerInfo = customerService.CheckIsBelongingByUsername(new GetCustInfoByUsernameCriteria
            {
                Username = getLogsParam.Username,
                UplineId = getLogsParam.UplineId
            });

            var isValidDownline = customerInfo != null && customerInfo.CustId > 0;
            if (!isValidDownline)
            {
                return pagingLogs;
            }

            pagingLogs.Logs = GetLogsFromDataAccess(getLogsParam, customerInfo);
            pagingLogs.TotalRecords = GetTotalRecordsOfLogs(getLogsParam, customerInfo.CustId);

            foreach (var log in pagingLogs.Logs)
            {
                log.ActionName = GetLogActionName(log.ActionId, getLogsParam.Language);
            }

            return pagingLogs;
        }

        public virtual List<Log> GetLogsFromDataAccess(LogParam getLogsParam, CustomerInfo customerInfo)
        {
            var getLogsCriteria = CreateGetLogsCriteria(getLogsParam, customerInfo.CustId);

            return dynamicRepository.Fetch<Log>(getLogsCriteria).ToList();
        }

        public abstract ICriteria CreateGetLogsCriteria(LogParam getLogsParam, int custId);

        public abstract ICriteria CreateTotalRecordsOfLogsCriteria(LogParam getLogsParam, int custId);

        private int GetTotalRecordsOfLogs(LogParam getLogsParam, int custId)
        {
            var getTotalRecordsOfLogsCriteria = CreateTotalRecordsOfLogsCriteria(getLogsParam, custId);

            return baseDataAccess.GetTotalRecordsOfLogs(getTotalRecordsOfLogsCriteria);
        }
    }
}

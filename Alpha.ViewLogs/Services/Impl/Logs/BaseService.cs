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
        public const string LogActionCacheKey = "Alpha.ViewLogs.CustomerSetting.LogAction";
        private const string LogActionTypesCacheKey = "Alpha.ViewLogs.CustomerSetting.LogActionTypes";
        private const int OneHour = 1;

        private readonly CachingManager cacheManager;
        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerService customerService;

        protected readonly LogDataAccess logDataAccess;

        protected BaseService(
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess logDataAccess)
        {
            this.cacheManager = cacheManager;
            this.dynamicRepository = dynamicRepository;
            this.customerService = customerService;
            this.logDataAccess = logDataAccess;
        }

        protected abstract ICriteria CreateGetLogsCriteria(LogParam logParam);

        protected abstract ICriteria CreateGetTotalLogsCriteria(LogParam logParam);


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

        public IEnumerable<LogActionType> GetLogActionTypes()
        {
            var logActionTypes = cacheManager.Get<IEnumerable<LogActionType>>(LogActionTypesCacheKey);

            if (logActionTypes != null 
                && logActionTypes.Any())
            {
                return logActionTypes;
            }

            logActionTypes = dynamicRepository.Fetch<LogActionType>(new GetAllLogActionTypeCriteria());

            cacheManager.Add(LogActionTypesCacheKey, logActionTypes);

            return logActionTypes;
        }

        public LogAction GetLogActionByActionId(int actionId)
        {
            var logActionDictionary = cacheManager.Get<Dictionary<int, LogAction>>(LogActionCacheKey);

            if (logActionDictionary == null || !logActionDictionary.Any())
            {
                logActionDictionary = new Dictionary<int, LogAction>();
                var logActions = dynamicRepository.Fetch<LogAction>(new GetLogActionsCriteria()).ToList();
                logActions.ForEach(logAction => logActionDictionary.Add(logAction.Id, logAction));
                cacheManager.Add(LogActionCacheKey, logActionDictionary, TimeSpan.FromHours(OneHour));
            }

            return logActionDictionary[actionId];
        }

        public PagingLogs<Log> GetLogs(LogParam logParam)
        {
            var pagingLogs = new PagingLogs<Log>();

            var downlineInfo = customerService.GetDownlineInfoByUsername(
                new GetCustInfoByUsernameCriteria
                {
                    Username = logParam.Username,
                    UplineId = logParam.UplineId
                });

            // If search username is not belong to searcher
            if (downlineInfo.CustId == 0)
            {
                return pagingLogs;
            }

            logParam.CustId = downlineInfo.CustId;

            pagingLogs.Logs = GetLogsFromDataAccess(logParam);
            pagingLogs.TotalRecords = GetTotalLogs(logParam);

            foreach (var log in pagingLogs.Logs)
            {
                log.ActionName = GetLogActionName(log.ActionId, logParam.Language);
            }

            return pagingLogs;
        }

        public virtual IEnumerable<Log> GetLogsFromDataAccess(LogParam logParam)
        {
            var criteria = CreateGetLogsCriteria(logParam);

            return dynamicRepository.Fetch<Log>(criteria);
        }

        private int GetTotalLogs(LogParam logParam)
        {
            var criteria = CreateGetTotalLogsCriteria(logParam);

            return logDataAccess.GetTotalRecordsOfLogs(criteria);
        }
    }
}

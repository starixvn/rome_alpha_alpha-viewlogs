namespace Alpha.ViewLogs.Services.Impl
{
    using Constants;
    using Criteria;
    using Fanex.Common.Helper;
    using Fanex.Data.Repository;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseLogServiceImpl : BaseLogService
    {
        public const string CacheKeyLogAction = "Alpha.ViewLogs.CustomerSetting.LogAction";
        private const int HourToCacheLogActions = 1;

        private readonly IDynamicRepository dynamicRepository;
        private readonly CachingManager cacheManager;

        public BaseLogServiceImpl(CachingManager cacheManager, IDynamicRepository dynamicRepository)
        {
            Check.Argument.IsNotNull(cacheManager, nameof(cacheManager));

            this.cacheManager = cacheManager;
            this.dynamicRepository = dynamicRepository;
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
    }
}

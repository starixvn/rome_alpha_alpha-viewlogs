namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria.CustomerSetting;
    using Fanex.Common.Helper;
    using Fanex.Data.Repository;
    using Models.CustomerSetting;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerSettingServiceImpl : CustomerSettingService
    {
        private const string CacheKeyLogActionTypes = "Alpha.ViewLogs.LogActionTypes";

        private readonly CachingManager cacheManager;
        private readonly IDynamicRepository dynamicRepository;

        public CustomerSettingServiceImpl(
            CachingManager cacheManager,
            IDynamicRepository dynamicRepository = null)
        {
            Check.Argument.IsNotNull(cacheManager, nameof(cacheManager));

            this.cacheManager = cacheManager;
            this.dynamicRepository = dynamicRepository ?? new DynamicRepository();
        }

        public List<LogActionType> LogActionTypes()
        {
            var logActionTypes = cacheManager.Get<List<LogActionType>>(CacheKeyLogActionTypes);
            if (logActionTypes != null && logActionTypes.Any())
            {
                return logActionTypes;
            }

            logActionTypes = dynamicRepository.Fetch<LogActionType>(new GetAllLogActionTypeCriteria()).ToList();

            return logActionTypes;
        }
    }
}

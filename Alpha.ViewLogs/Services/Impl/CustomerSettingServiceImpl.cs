namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria.CustomerSetting;
    using Fanex.Common.Helper;
    using Fanex.Data.Repository;
    using Models.CustomerSetting;
    using Params.CustomerSetting;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerSettingServiceImpl : CustomerSettingService
    {
        private const string CacheKeyLogActionTypes = "Alpha.ViewLogs.CustomerSetting.LogActionTypes";
        private const string CacheKeyTransformObject = "Alpha.ViewLogs.CustomerSetting.TransformObject";

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
            cacheManager.Add(CacheKeyLogActionTypes, logActionTypes);

            return logActionTypes;
        }

        public void GetLogs(GetLogsParam getLogsParam)
        {
            if (getLogsParam.IsDebug)
            {
                cacheManager.Remove(CacheKeyTransformObject);
            }
        }
    }
}

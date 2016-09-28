namespace Alpha.ViewLogs.Tests.Services
{
    using Alpha.ViewLogs.Services.Impl;
    using Criteria.Setting;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Params;
    using ViewLogs.Services;

    public class BaseServiceForTesting : BaseService
    {
        public BaseServiceForTesting(
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess logDataAccess) :
            base(dynamicRepository, customerService, logDataAccess)
        {
        }

        protected override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            return new GetSettingLogsCriteria();
        }

        protected override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            return new GetTotalSettingLogsCriteria();
        }
    }
}

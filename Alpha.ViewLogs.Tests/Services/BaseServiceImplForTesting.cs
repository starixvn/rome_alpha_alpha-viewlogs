namespace Alpha.ViewLogs.Tests.Services
{
    using System;
    using Criteria.Setting;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Params;
    using ViewLogs.Services;
    using ViewLogs.Services.Impl;

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
            return new GetSettingLogsCriteria(0, string.Empty, 0, DateTime.Now, DateTime.Now, false, 0, 0);
        }

        protected override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            return new GetTotalSettingLogsCriteria(0, 0, DateTime.Now, DateTime.Now, false);
        }
    }
}

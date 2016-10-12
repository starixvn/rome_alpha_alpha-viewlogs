namespace Alpha.ViewLogs.Tests.Services.Logs
{
    using System;
    using Criteria.Setting;
    using Fanex.Data.Repository;
    using Params;
    using ViewLogs.DataAccesses;
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

        internal override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            return new GetSettingLogsCriteria(0, string.Empty, 0, DateTime.Now, DateTime.Now, false, 0, 0);
        }

        internal override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            return new GetTotalSettingLogsCriteria(0, 0, DateTime.Now, DateTime.Now, false);
        }
    }
}

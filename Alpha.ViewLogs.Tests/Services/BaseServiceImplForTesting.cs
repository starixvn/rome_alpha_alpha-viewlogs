namespace Alpha.ViewLogs.Tests.Services
{
    using System;
    using Alpha.ViewLogs.Services.Impl;
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
            throw new NotImplementedException();
        }

        protected override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            throw new NotImplementedException();
        }
    }
}

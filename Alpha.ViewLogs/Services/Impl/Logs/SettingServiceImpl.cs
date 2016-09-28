namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
    using Criteria.Setting;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;

    public class SettingServiceImpl : BaseService, LogService
    {
        public SettingServiceImpl(
             IDynamicRepository dynamicRepository,
             CustomerService customerService,
             LogDataAccess baseDataAccess) :
            base(dynamicRepository, customerService, baseDataAccess)
        {
        }

        protected override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetSettingLogsCriteria
            {
                ActionTypeId = ((SettingParam)logParam).ActionTypeId,
                CustId = logParam.CustId,
                CustName = logParam.Username,
                FromDate = DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                IsInternal = logParam.IsInternal,
                PageSize = logParam.PageSize,
                PageIndex = logParam.PageIndex
            };
        }

        protected override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetTotalSettingLogsCriteria
            {
                CustId = logParam.CustId,
                FromDate = DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                ToDate = DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                IsInternal = logParam.IsInternal
            };
        }
    }
}

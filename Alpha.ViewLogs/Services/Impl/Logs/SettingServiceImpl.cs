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

            return new GetSettingLogsCriteria(
                logParam.CustId,
                logParam.Username,
                ((SettingParam)logParam).ActionTypeId,
                DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal,
                logParam.PageIndex,
                logParam.PageSize);
        }

        protected override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException("logParam");
            }

            return new GetTotalSettingLogsCriteria(
                logParam.CustId,
                ((SettingParam)logParam).ActionTypeId,
                DateTimeHelper.GetBeginningOfDate(logParam.FromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),
                logParam.IsInternal);
        }
    }
}

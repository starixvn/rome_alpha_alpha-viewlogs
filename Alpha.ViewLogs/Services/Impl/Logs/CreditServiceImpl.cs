namespace Alpha.ViewLogs.Services.Impl.Logs
{
    using System;
    using Alpha.ViewLogs.Models;
    using Criteria.Credit;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Helpers;
    using Params;
    using Sunplus.Agent.Model.Entities;

    public class CreditServiceImpl : BaseService, LogService
    {
        public CreditServiceImpl(
            IDynamicRepository dynamicRepository,
            CustomerService customerService,
            LogDataAccess baseDataAccess,
            IUserSession session) :
            base(dynamicRepository, customerService, baseDataAccess, session)
        {
        }

        public override PagingLogs<Log> GetPagingLogs(LogParam logParam)
        {
            var pagingLogs = new PagingLogs<Log>();
            if (logParam == null)
            {
                return pagingLogs;
            }

            pagingLogs.Logs = GetLogs(logParam);
            pagingLogs.TotalRecords = GetTotalLogs(logParam);

            foreach (var log in pagingLogs.Logs)
            {
                var isHideNickname = IsHideNickName(IsSubAccFromLog(log), log.RoleId, log.CustId, SubAccNameFromLog(log));
                log.IsHideNickname = isHideNickname;
            }

            return pagingLogs;
        }

        internal override ICriteria CreateGetLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException(nameof(logParam));
            }

            var fromDate = GetGreaterDate(logParam.FromDate, logParam.LimitedDateToShowInfo);

            return new GetCreditLogsCriteria(
                logParam.ActorId,
                logParam.RoleId,
                logParam.Username,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate),                
                logParam.PageIndex,
                logParam.PageSize);
        }

        internal override ICriteria CreateGetTotalLogsCriteria(LogParam logParam)
        {
            if (logParam == null)
            {
                throw new ArgumentNullException(nameof(logParam));
            }

            var fromDate = GetGreaterDate(logParam.FromDate, logParam.LimitedDateToShowInfo);

            return new GetTotalCreditLogsCriteria(
                logParam.ActorId,
                logParam.RoleId,
                logParam.Username,
                DateTimeHelper.GetBeginningOfDate(fromDate),
                DateTimeHelper.GetEndingOfDate(logParam.ToDate));
        }
    }
}

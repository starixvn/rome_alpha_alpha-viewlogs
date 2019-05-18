namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria;
    using DataAccesses;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models;
    using Params;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Base;
    using System.Text.RegularExpressions;

    public abstract class BaseService
    {
        private const string LogActionCacheKey = "Alpha.ViewLogs.Setting.LogAction";
        private const string LogActionTypesCacheKey = "Alpha.ViewLogs.Setting.LogActionTypes";
        private const int OneHour = 1;

        private readonly IDynamicRepository dynamicRepository;
        private readonly CustomerService customerService;
        private readonly LogDataAccess logDataAccess;
        private readonly IUserSession session;

        protected BaseService(IDynamicRepository dynamicRepository, CustomerService customerService, LogDataAccess logDataAccess, IUserSession session = null)
        {
            this.dynamicRepository = dynamicRepository;
            this.customerService = customerService;
            this.logDataAccess = logDataAccess;
            this.session = session ?? new UserSessionExt();
        }

        protected LogDataAccess LogDataAccess
        {
            get
            {
                return logDataAccess;
            }
        }

        internal abstract ICriteria CreateGetLogsCriteria(LogParam logParam);

        internal abstract ICriteria CreateGetTotalLogsCriteria(LogParam logParam);

        public string GetLogActionName(int actionId, string language)
        {
            var logAction = GetLogAction(actionId);
            switch (language)
            {
                case LanguageCode.ChineseTraditional:
                    return logAction.ChineseTraditionalName;

                case LanguageCode.ChineseSimplified:
                    return logAction.ChineseSimplifiedName;

                case LanguageCode.Japanese:
                    return logAction.JapaneseName;

                case LanguageCode.Thai:
                    return logAction.ThaiName;

                case LanguageCode.Korean:
                    return logAction.KoreanName;

                case LanguageCode.Vietnamese:
                    return logAction.VietnameseName;

                default:
                    return logAction.EnglishName;
            }
        }

        public IEnumerable<LogActionType> GetLogActionTypes()
        {
            var logActionTypes = ConfiguredCacheService.Get<IEnumerable<LogActionType>>(LogActionTypesCacheKey);

            if (logActionTypes != null
                && logActionTypes.Any())
            {
                return logActionTypes;
            }

            logActionTypes = dynamicRepository.Fetch<LogActionType>(new GetAllLogActionTypeCriteria());

            ConfiguredCacheService.Set(LogActionTypesCacheKey, logActionTypes, new CacheItemOptions { SlidingExpiration = TimeSpan.FromHours(OneHour) });

            return logActionTypes;
        }

        public LogAction GetLogAction(int actionId)
        {
            var logActionDictionary = ConfiguredCacheService.Get<Dictionary<int, LogAction>>(LogActionCacheKey);

            if (logActionDictionary == null
                || !logActionDictionary.Any())
            {
                logActionDictionary = dynamicRepository
                    .Fetch<LogAction>(new GetLogActionsCriteria())
                    .ToDictionary(logAction => logAction.Id);

                ConfiguredCacheService.Set(LogActionCacheKey, logActionDictionary, DefaultVariables.CacheItemOptions);
            }

            return logActionDictionary.ContainsKey(actionId) ?
                logActionDictionary[actionId] :
                new LogAction
                {
                    Id = actionId
                };
        }

        public PagingLogs<Log> GetPagingLogs(LogParam logParam)
        {
            var pagingLogs = new PagingLogs<Log>();
            if (logParam == null)
            {
                return pagingLogs;
            }

            var downlineInfo = customerService.GetDownlineInfoByUsername(logParam.Username, logParam.UplineId);

            // If search username is not belong to searcher, then return
            if (downlineInfo.CustId == 0)
            {
                return pagingLogs;
            }

            logParam.CustId = downlineInfo.CustId;

            pagingLogs.Logs = GetLogs(logParam);
            pagingLogs.TotalRecords = GetTotalLogs(logParam);

            foreach (var log in pagingLogs.Logs)
            {
                var isHideNickname = IsHideNickName(IsSubAccFromLog(log), downlineInfo.RoleId, downlineInfo.CustId);
                log.IsHideNickname = isHideNickname;
            }
            
            return pagingLogs;
        }

        protected DateTime GetGreaterDate(DateTime fromDate, DateTime limitedDate)
            => fromDate < limitedDate ? limitedDate : fromDate;

        protected virtual IEnumerable<Log> GetLogs(LogParam logParam)
        {
            var criteria = CreateGetLogsCriteria(logParam);
            return dynamicRepository.Fetch<Log>(criteria);
        }

        private int GetTotalLogs(LogParam logParam)
        {
            var criteria = CreateGetTotalLogsCriteria(logParam);

            return LogDataAccess.GetTotalRecordsOfLogs(criteria);
        }

        private bool IsHideNickName(bool isSubAccount, int roleId, int custId)
            => IsIndirect(roleId) || IsSubAccViewMainAcc(isSubAccount, custId) || IsDirectUplineViewSubAcc(isSubAccount, roleId);

        private bool IsIndirect (int roleId)
            => session.RoleId - roleId != 1 && session.RoleId != roleId;

        private bool IsDirectUplineViewSubAcc(bool isSubAccount, int roleId)
            => session.RoleId - roleId == 1 && isSubAccount;

        private bool IsSubAccViewMainAcc (bool isSubAccount, int custId)
            => session.IsSubAcc && session.CustId == custId && !isSubAccount;

        private static bool IsSubAccFromLog(Log log)
            => NicknameActionIds.Any(actionId => actionId == log.ActionId) 
                && new Regex($"(?:SubAccName=)(?<paramValue>[^&|^\\s]+)").IsMatch(log.TrackingValue);

        private static IEnumerable<int> NicknameActionIds
            => new List<int> { 621, 688};
    }
}

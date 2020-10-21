namespace Alpha.ViewLogs.Site.Requests.Credit
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Helpers;
    using ViewModels;

    public class IndexQuery : LogQuery, IRequest<LogViewModel>
    {
        public IndexQuery() : this(null, null, null)
        {
        }

        public IndexQuery(
            IUserSession currentUserSession,
            IDateTime dateTimeWrap,
            bool? isShowOldValue,
            Dictionary<string, bool> sportBetTypeVisibilities = null,
            Dictionary<string, int> resourcesByBetTypeCacheRef = null,
            bool? isShowMyanmarOdds = null,
            Dictionary<string, int> resourcesByProudctCacheRef = null) :
            base(
                currentUserSession,
                dateTimeWrap,
                isShowOldValue,
                sportBetTypeVisibilities,
                resourcesByBetTypeCacheRef,
                isShowMyanmarOdds,
                resourcesByProudctCacheRef)
        {
        }
    }
}
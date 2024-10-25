namespace Alpha.ViewLogs.Site.Requests.Credit
{
    using System.Collections.Generic;
    using AlphaPlus.User.Models;
    using MediatR;
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
            Dictionary<string, int> resourcesByProudctCacheRef = null) :
            base(
                currentUserSession,
                dateTimeWrap,
                isShowOldValue,
                sportBetTypeVisibilities,
                resourcesByBetTypeCacheRef,
                resourcesByProudctCacheRef)
        {
        }
    }
}
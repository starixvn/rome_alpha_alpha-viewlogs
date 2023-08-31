namespace Alpha.ViewLogs.Site.Requests.Login
{
    using System.Collections.Generic;
    using AlphaPlus.User.Models;
    using MediatR;
    using ViewLogs.Helpers;
    using ViewModels.Login;

    public class IndexQuery : LogQuery, IRequest<IndexViewModel>
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
            Dictionary<string, int> resourcesByProductCacheRef = null) :
            base(
                currentUserSession,
                dateTimeWrap,
                isShowOldValue,
                sportBetTypeVisibilities,
                resourcesByBetTypeCacheRef,
                isShowMyanmarOdds,
                resourcesByProductCacheRef)
        {
        }

        public bool IsAgentViewing { get; set; }
    }
}
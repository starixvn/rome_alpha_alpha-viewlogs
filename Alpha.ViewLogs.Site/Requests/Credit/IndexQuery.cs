namespace Alpha.ViewLogs.Site.Requests.Credit
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    public class IndexQuery : LogQuery, IRequest<LogViewModel>
    {
        public IndexQuery() : this(null, null, null, null, null, null)
        {
        }

        public IndexQuery(
            IUserSession currentUserSession,
            bool? isShowOldValue,
            Dictionary<int, bool> actionIdVisibilities = null,
            Dictionary<string, bool> sportBetTypeVisibilities = null,
            Dictionary<string, int> resourcesByBetTypeCacheRef = null,
            bool? isShowMyanmarOdds = null) :
            base(
                currentUserSession,
                isShowOldValue,
                actionIdVisibilities,
                sportBetTypeVisibilities,
                resourcesByBetTypeCacheRef,
                isShowMyanmarOdds)
        {
        }
    }
}
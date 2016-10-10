namespace Alpha.ViewLogs.Site.Requests.Credit
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Helpers;
    using ViewModels;

    public class IndexQuery : LogQuery, IRequest<LogViewModel>
    {
        [ExcludeFromCodeCoverage]
        public IndexQuery() : this(null, null, null)
        {
        }

        public IndexQuery(
            IUserSession currentUserSession,
            IDateTime dateTimeWrap,
            bool? isShowOldValue,
            Dictionary<int, bool> actionIdVisibilities = null,
            Dictionary<string, bool> sportBetTypeVisibilities = null,
            Dictionary<string, int> resourcesByBetTypeCacheRef = null,
            bool? isShowMyanmarOdds = null) :
            base(
                currentUserSession,
                dateTimeWrap,
                isShowOldValue,
                actionIdVisibilities,
                sportBetTypeVisibilities,
                resourcesByBetTypeCacheRef,
                isShowMyanmarOdds)
        {
        }
    }
}
namespace Alpha.ViewLogs.Site.Requests.Setting
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Helpers;
    using ViewModels.Setting;

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

        public string TrackingValueTransformTemplatePath { get; set; }
    }
}
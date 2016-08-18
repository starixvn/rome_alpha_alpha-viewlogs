namespace Alpha.ViewLogs.Site.Requests.CustomerSetting
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewModels.CustomerSetting;

    public class IndexQuery : LogQuery, IRequest<IndexViewModel>
    {
        public IndexQuery() : this(null, null, null, null)
        {
        }

        public IndexQuery(
            IUserSession currentUserSession,
            bool? isShowOldValue,
            Dictionary<int, bool> actionIdVisibilities = null,
            Dictionary<string, bool> sportBetTypeVisibilities = null) :
            base(currentUserSession, isShowOldValue, actionIdVisibilities, sportBetTypeVisibilities)
        {
        }

        public string TrackingValueTransformTemplatePath { get; set; }
    }
}
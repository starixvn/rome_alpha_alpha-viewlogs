namespace Alpha.ViewLogs.Site.Requests.Status
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    public class IndexQuery : LogQuery, IRequest<LogViewModel>
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
namespace Alpha.ViewLogs.Site.Requests.Login
{
    using System.Collections.Generic;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewModels.Login;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
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

        public bool IsAgentViewing { get; set; }
    }
}
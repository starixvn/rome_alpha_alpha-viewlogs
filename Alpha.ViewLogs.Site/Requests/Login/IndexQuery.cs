namespace Alpha.ViewLogs.Site.Requests.Login
{
    using System.Collections.Generic;
    using MediatR;
    using Models.Enumerations;
    using Sunplus.Agent.Model.Entities;
    using ViewModels.Login;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        private readonly bool isAgentViewing;

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
            currentUserSession = currentUserSession ?? UserSession.Current;
            this.isAgentViewing = currentUserSession.RoleId == UserLevel.Agent.Value;
            this.Username = isAgentViewing ? currentUserSession.UserName : this.Username;
            this.IsInternal = currentUserSession.IsInternal;
        }

        public bool IsAgentViewing
        {
            get
            {
                return isAgentViewing;
            }
        }
    }
}
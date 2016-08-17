namespace Alpha.ViewLogs.Site.Controllers
{
    using MediatR;
    using Models.Enumerations;
    using Requests.Login;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class LoginController : BaseController
    {
        private readonly IMediator mediator;

        public LoginController(IUserSession userSession, IMediator mediator)
        {
            CurrentUserSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.AssignServerSideParam(CurrentUserSession, GetShowOldValue());

            var isAgent = CurrentUserSession.RoleId == UserLevel.Agent.Value;
            indexQuery.Username = isAgent ? CurrentUserSession.UserName : indexQuery.Username;
            indexQuery.IsInternal = CurrentUserSession.IsInternal;

            var viewModel = mediator.Send(indexQuery);
            viewModel.ShowOldValuesCookieName = CookieShowOldValue;

            return View(viewModel);
        }
    }
}
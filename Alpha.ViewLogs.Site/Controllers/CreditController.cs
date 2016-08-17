namespace Alpha.ViewLogs.Site.Controllers
{
    using MediatR;
    using Requests.Credit;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class CreditController : BaseController
    {
        private readonly IMediator mediator;

        public CreditController(IUserSession userSession, IMediator mediator)
        {
            CurrentUserSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.AssignServerSideParam(CurrentUserSession, GetShowOldValue());

            var viewModel = mediator.Send(indexQuery);
            viewModel.ShowOldValuesCookieName = CookieShowOldValue;

            return View(viewModel);
        }
    }
}
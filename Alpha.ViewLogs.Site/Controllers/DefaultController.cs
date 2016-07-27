namespace Alpha.ViewLogs.Site.Controllers
{
    using MediatR;
    using Requests;
    using SecurityCode.Sessions;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class DefaultController : BaseController
    {
        private readonly IUserSession userSession;
        private readonly ISecurityCodeSession securityCodeSession;
        private readonly IMediator mediator;

        public DefaultController(
            IUserSession userSession,
            ISecurityCodeSession securityCodeSession,
            IMediator mediator)
        {
            this.userSession = userSession;
            this.securityCodeSession = securityCodeSession;
            this.mediator = mediator;
        }

        public ActionResult Index()
        {
            var viewModel = mediator.Send(new IndexQuery());

            return View(viewModel);
        }
    }
}
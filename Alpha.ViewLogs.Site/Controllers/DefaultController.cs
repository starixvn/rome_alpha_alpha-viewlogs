namespace Alpha.ViewLogs.Site.Controllers
{
    using MediatR;
    using Requests;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class DefaultController : BaseController
    {
        private readonly IUserSession userSession;
        private readonly IMediator mediator;

        public DefaultController(IUserSession userSession, IMediator mediator)
        {
            this.userSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index()
        {
            var viewModel = mediator.Send(new IndexQuery());

            return View(viewModel);
        }
    }
}
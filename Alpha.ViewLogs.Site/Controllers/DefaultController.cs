namespace Alpha.ViewLogs.Site.Controllers
{
    using MediatR;
    using Requests;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class DefaultController : BaseController
    {
        private readonly IMediator mediator;

        public DefaultController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index()
        {
            var viewModel = mediator.Send(new IndexQuery());

            return View(viewModel);
        }
    }
}
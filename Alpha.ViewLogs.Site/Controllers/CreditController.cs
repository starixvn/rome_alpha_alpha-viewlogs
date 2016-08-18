namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.Credit;

    public class CreditController : BaseController
    {
        private readonly IMediator mediator;

        public CreditController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
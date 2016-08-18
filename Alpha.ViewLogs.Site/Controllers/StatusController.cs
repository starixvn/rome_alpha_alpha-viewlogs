namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.Status;

    public class StatusController : BaseController
    {
        private readonly IMediator mediator;

        public StatusController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath("~/App_Data/TrackingValueTransformTemplates");

            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
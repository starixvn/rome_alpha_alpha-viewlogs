namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.Status;
    using Sunplus.Agent.Model.Entities;
    using ViewModels.Status;

    public class StatusController : BaseController
    {
        private readonly IMediator mediator;

        public StatusController(IUserSession userSession, IMediator mediator)
        {
            CurrentUserSession = userSession;
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
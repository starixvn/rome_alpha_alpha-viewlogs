namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.Setting;

    public class SettingController : BaseController
    {
        private const string TemplatePath = "~/App_Data/TrackingValueTransformTemplates";
        private readonly IMediator mediator;

        public SettingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath(TemplatePath);

            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
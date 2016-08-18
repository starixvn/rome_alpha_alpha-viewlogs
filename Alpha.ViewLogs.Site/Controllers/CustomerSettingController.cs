namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.CustomerSetting;

    public class CustomerSettingController : BaseController
    {
        private readonly IMediator mediator;

        public CustomerSettingController(IMediator mediator)
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
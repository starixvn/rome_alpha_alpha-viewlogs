namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.CustomerSetting;
    using Sunplus.Agent.Model.Entities;

    public class CustomerSettingController : BaseController
    {
        private readonly IMediator mediator;

        public CustomerSettingController(IUserSession userSession, IMediator mediator)
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
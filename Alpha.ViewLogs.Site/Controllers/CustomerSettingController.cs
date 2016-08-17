namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using MediatR;
    using Requests.CustomerSetting;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using ViewModels.CustomerSetting;

    [SessionState(SessionStateBehavior.Default)]
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
            indexQuery.AssignServerSideParam(CurrentUserSession, GetShowOldValue());

            indexQuery.IsDebug = AppSettings.IsDebug;
            indexQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath("~/App_Data/TrackingValueTransformTemplates");

            var viewModel = mediator.Send<IndexViewModel>(indexQuery);
            viewModel.ShowOldValuesCookieName = CookieShowOldValue;

            return View(viewModel);
        }
    }
}
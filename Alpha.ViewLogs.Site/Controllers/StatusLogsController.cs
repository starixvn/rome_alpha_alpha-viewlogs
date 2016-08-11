namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using MediatR;
    using Requests.StatusLogs;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using ViewModels.StatusLogs;

    [SessionState(SessionStateBehavior.Default)]
    public class StatusLogsController : BaseController
    {
        private readonly IMediator mediator;

        public StatusLogsController(IUserSession userSession, IMediator mediator)
        {
            CurrentUserSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            if (!CanViewSettingLogs(CurrentUserSession))
            {
                Response.Redirect(LoginLogsURL);
            }

            AssignServerSideParamForGetLogsQuery(indexQuery);

            indexQuery.IsDebug = AppSettings.IsDebug;
            indexQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath("~/App_Data/TrackingValueTransformTemplates");

            var viewModel = mediator.Send<IndexViewModel>(indexQuery);
            viewModel.ShowOldValuesCookieName = CookieShowOldValue;

            return View(viewModel);
        }
    }
}
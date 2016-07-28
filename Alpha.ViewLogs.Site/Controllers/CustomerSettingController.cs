namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using Helpers;
    using MediatR;
    using Requests;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class CustomerSettingController : BaseController
    {
        private readonly IUserSession userSession;
        private readonly IMediator mediator;

        public CustomerSettingController(IUserSession userSession, IMediator mediator)
        {
            this.userSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(CustomerSettingQuery customerSettingQuery)
        {
            if (!userSession.SubPageAccess.ViewLog)
            {
                Response.Redirect(MicroSites.OldMainUrl(AppSettings.AccessDeniedPage));
            }

            customerSettingQuery.IsDebug = AppSettings.IsDebug;
            customerSettingQuery.HasViewLogPermission = userSession.SubPageAccess != null && userSession.SubPageAccess.ViewLog;
            customerSettingQuery.Language = userSession.Language;

            var viewModel = mediator.Send(customerSettingQuery);

            return View(viewModel);
        }
    }
}
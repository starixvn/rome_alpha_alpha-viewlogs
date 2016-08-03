namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using MediatR;
    using Requests;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using ViewLogs.Helpers;

    [SessionState(SessionStateBehavior.Default)]
    public class CustomerSettingLogsController : BaseController
    {
        private const string CookieShowOldValue = "showOldValue";

        private readonly IUserSession userSession;
        private readonly IMediator mediator;

        public CustomerSettingLogsController(IUserSession userSession, IMediator mediator)
        {
            this.userSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(CustomerSettingLogsQuery customerSettingLogsQuery)
        {
            if (!userSession.SubPageAccess.ViewLog)
            {
                Response.Redirect(MicroSites.OldMainUrl(AppSettings.AccessDeniedPage));
            }

            customerSettingLogsQuery.IsDebug = AppSettings.IsDebug;
            customerSettingLogsQuery.HasViewLogPermission = userSession.SubPageAccess != null && userSession.SubPageAccess.ViewLog;
            customerSettingLogsQuery.Language = userSession.Language;
            customerSettingLogsQuery.IsInternal = false;
            customerSettingLogsQuery.IsShowOldValue = GetShowOldValue();
            customerSettingLogsQuery.UplineId = userSession.CustId;
            customerSettingLogsQuery.NewFormatActionLogIds = AppSettings.NewFormatActionLogIds;
            customerSettingLogsQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath("~/App_Data/TrackingValueTransformTemplates");
            customerSettingLogsQuery.IsShowMyanmarOdds = UserPermission.IsShowMyanmarOdds;

            var viewModel = mediator.Send(customerSettingLogsQuery);

            return View(viewModel);
        }

        private bool GetShowOldValue()
        {
            bool showOldValue = true;
            var cookie = Request.Cookies.Get(CookieShowOldValue);
            if (cookie != null)
            {
                bool.TryParse(cookie.Value, out showOldValue);
            }

            return showOldValue;
        }
    }
}
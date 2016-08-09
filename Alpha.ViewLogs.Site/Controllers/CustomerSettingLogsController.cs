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
    public class CustomerSettingLogsController : BaseViewLogController
    {
        private const string CookieShowOldValue = "Alpha.ViewLogs.ShowOldValuesCookie";

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

            if (!CanViewSettingLogs(userSession))
            {
                Response.Redirect(LoginLogsURL);
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
            viewModel.ShowOldValuesCookieName = CookieShowOldValue;

            return View(viewModel);
        }

        private bool GetShowOldValue()
        {
            bool showOldValue = true;
            var cookie = Request.Cookies.Get(CookieShowOldValue);

            return cookie != null && bool.TryParse(cookie.Value, out showOldValue) ? showOldValue : true;
        }
    }
}
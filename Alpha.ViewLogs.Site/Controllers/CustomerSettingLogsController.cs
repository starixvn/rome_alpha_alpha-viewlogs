namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using MediatR;
    using Requests.CustomerSettingLogs;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using ViewModels.CustomerSettingLogs;

    [SessionState(SessionStateBehavior.Default)]
    public class CustomerSettingLogsController : BaseController
    {
        private const string CookieShowOldValue = "Alpha.ViewLogs.ShowOldValuesCookie";

        private readonly IUserSession userSession;
        private readonly IMediator mediator;

        public CustomerSettingLogsController(IUserSession userSession, IMediator mediator)
        {
            this.userSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            if (!CanViewSettingLogs(userSession))
            {
                Response.Redirect(LoginLogsURL);
            }

            AssignServerSideParamForCustomerSettingLogsQuery(indexQuery);

            var viewModel = mediator.Send<IndexViewModel>(indexQuery);
            viewModel.ShowOldValuesCookieName = CookieShowOldValue;

            return View(viewModel);
        }

        public JsonResult GetLogs(GetLogsQuery getLogsQuery)
        {
            if (!CanViewSettingLogs(userSession))
            {
                Response.Redirect(LoginLogsURL);
            }

            AssignServerSideParamForCustomerSettingLogsQuery(getLogsQuery);

            var viewModel = mediator.Send<GetLogsViewModel>(getLogsQuery);

            return Json(viewModel);
        }

        private void AssignServerSideParamForCustomerSettingLogsQuery(GetLogsQuery getLogsQuery)
        {
            getLogsQuery.RoleId = userSession.RoleId;
            getLogsQuery.IsDebug = AppSettings.IsDebug;
            getLogsQuery.HasViewLogPermission = userSession.SubPageAccess != null && userSession.SubPageAccess.ViewLog;
            getLogsQuery.Language = userSession.Language;
            getLogsQuery.IsInternal = false;
            getLogsQuery.IsShowOldValue = GetShowOldValue();
            getLogsQuery.UplineId = userSession.CustId;
            getLogsQuery.NewFormatActionLogIds = AppSettings.NewFormatActionLogIds;
            getLogsQuery.TrackingValueTransformTemplatePath = HttpContext.Server.MapPath("~/App_Data/TrackingValueTransformTemplates");
            getLogsQuery.IsShowMyanmarOdds = UserPermission.IsShowMyanmarOdds;
        }

        private bool GetShowOldValue()
        {
            bool showOldValue = true;
            var cookie = Request.Cookies.Get(CookieShowOldValue);

            return cookie != null && bool.TryParse(cookie.Value, out showOldValue) ? showOldValue : true;
        }
    }
}
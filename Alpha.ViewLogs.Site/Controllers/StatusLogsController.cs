namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using Fanex.Common;
    using MediatR;
    using Requests.StatusLogs;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using ViewModels.StatusLogs;

    [SessionState(SessionStateBehavior.Default)]
    public class StatusLogsController : BaseController
    {
        private const string CookieShowOldValue = "Alpha.ViewLogs.ShowOldValuesCookie";

        private readonly IUserSession userSession;
        private readonly IMediator mediator;

        public StatusLogsController(IUserSession userSession, IMediator mediator)
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

            AssignServerSideParamForGetLogsQuery(indexQuery);

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

            AssignServerSideParamForGetLogsQuery(getLogsQuery);

            var viewModel = mediator.Send<GetLogsViewModel>(getLogsQuery);

            return Json(viewModel);
        }

        private void AssignServerSideParamForGetLogsQuery(GetLogsQuery getLogsQuery)
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
            var cookie = Request.Cookies.Get(CookieShowOldValue);

            bool showOldValue = cookie == null || Converter.ToInt(cookie.Value) == 1;

            return showOldValue;
        }
    }
}
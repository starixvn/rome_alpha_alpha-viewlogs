namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using Fanex.Common;
    using Requests;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.SessionState;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class BaseController : Controller
    {
        private IUserSession currentUserSession;
        protected const string CookieShowOldValue = "Alpha.ViewLogs.ShowOldValuesCookie";
        protected const string LoginLogsURL = "~/LoginLogs";
        private CultureInfo defaultCulture = CultureInfo.CreateSpecificCulture(Language.Default);

        public IUserSession CurrentUserSession
        {
            get
            {
                return currentUserSession;
            }

            set
            {
                currentUserSession = value;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Reviewed!")]
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                UserSession.Current.UserId <= 0)
            {
                var logInUrl = MicroSites.OldMainUrl(FormsAuthentication.LoginUrl);
                Response.Redirect(logInUrl, true);
                return;
            }

            if (!UserSession.Current.SubPageAccess.ViewLog)
            {
                Response.Redirect(MicroSites.OldMainUrl(AppSettings.AccessDeniedPage));
            }
        }

        protected override JsonResult Json(
            object data,
            string contentType,
            Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = int.MaxValue
            };
        }

        protected override void Initialize(RequestContext requestContext)
        {
            var cultureName = UserSession.Current.Language;

            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = Language.Default;
                UserSession.Current.Language = cultureName;
            }

            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = defaultCulture.DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = defaultCulture.NumberFormat;

            base.Initialize(requestContext);
        }

        protected bool CanViewSettingLogs(IUserSession userSession)
        {
            return userSession.SubAccId <= 0 ||
                (userSession.SubPageAccess != null && userSession.SubPageAccess.ViewLog);
        }

        protected bool GetShowOldValue()
        {
            var cookie = Request.Cookies.Get(CookieShowOldValue);

            bool showOldValue = cookie == null || Converter.ToInt(cookie.Value) == 1;

            return showOldValue;
        }

        protected virtual void AssignServerSideParamForGetLogsQuery(BaseQuery getLogsQuery)
        {
            getLogsQuery.RoleId = CurrentUserSession.RoleId;
            getLogsQuery.HasViewLogPermission = CurrentUserSession.SubPageAccess != null && CurrentUserSession.SubPageAccess.ViewLog;
            getLogsQuery.Language = CurrentUserSession.Language;
            getLogsQuery.IsInternal = false;
            getLogsQuery.IsShowOldValue = GetShowOldValue();
            getLogsQuery.UplineId = CurrentUserSession.CustId;
            getLogsQuery.NewFormatActionLogIds = AppSettings.NewFormatActionLogIds;
            getLogsQuery.IsShowMyanmarOdds = UserPermission.IsShowMyanmarOdds;
        }
    }
}
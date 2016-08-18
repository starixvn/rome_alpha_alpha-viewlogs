namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.SessionState;
    using Configurations;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Constants;
    using ViewLogs.Helpers;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class BaseController : Controller
    {
        private IUserSession currentUserSession;
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

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                CurrentUserSession.UserId <= 0)
            {
                var logInUrl = MicroSites.OldMainPath(FormsAuthentication.LoginUrl);
                Response.Redirect(logInUrl, true);
                return;
            }

            if (CurrentUserSession.IsSubAcc && !CurrentUserSession.SubPageAccess.ViewLog)
            {
                Response.Redirect(MicroSites.OldMainPath(AppSettings.AccessDeniedPage));
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
            var cultureName = CurrentUserSession.Language;

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
    }
}
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
    using Helpers;
    using Sunplus.Agent.Model.Entities;
    using ViewLogs.Constants;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public abstract class BaseController : Controller
    {
        private CultureInfo defaultCulture = CultureInfo.CreateSpecificCulture(LanguageCode.Default);
        private MicroSites microSites;

        protected BaseController(IUserSession userSession, MicroSites microSites)
        {
            UserSession = userSession;
            this.microSites = microSites;
        }

        public IUserSession UserSession { get; set; }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            base.OnAuthentication(filterContext);

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated
                 || UserSession.UserId <= 0)
            {
                UnAuthentication(filterContext);
            }

            if (UserSession.IsSubAcc && !UserSession.SubPageAccess.ViewLog)
            {
                Response.Redirect(microSites.OldMainPath(AppSettings.AccessDeniedPage));
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
            var cultureName = UserSession.Language;

            if (string.IsNullOrWhiteSpace(cultureName))
            {
                cultureName = LanguageCode.Default;
                UserSession.Language = cultureName;
            }

            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture.DateTimeFormat = defaultCulture.DateTimeFormat;
            Thread.CurrentThread.CurrentCulture.NumberFormat = defaultCulture.NumberFormat;

            base.Initialize(requestContext);
        }

        private void UnAuthentication(AuthenticationContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                response.Clear();
                response.Write("{ \"message\": \"You are not authorized on this function!\" }");
                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
                response.TrySkipIisCustomErrors = true;
                response.End();
            }
            else
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.LoginUrl);
            }
        }
    }
}
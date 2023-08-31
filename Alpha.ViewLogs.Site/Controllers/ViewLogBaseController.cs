namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using AlphaPlus.User.Models;
    using Configurations;
    using Helpers;
    using Web;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public abstract class ViewLogBaseController : BaseController
    {
        private readonly MicroSites microSites;

        protected ViewLogBaseController(IUserSession userSession, MicroSites microSites)
        {
            UserSession = userSession;
            this.microSites = microSites;
        }

        public IUserSession UserSession { get; set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (UserSession.IsSubAcc && !UserSession.SubPageAccess.ViewLog)
            {
                Response.Redirect(microSites.OldMainPath(AppSettings.AccessDeniedPage));
            }
        }
    }
}
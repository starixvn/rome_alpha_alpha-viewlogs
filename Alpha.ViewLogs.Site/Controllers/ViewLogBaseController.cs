namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Configurations;
    using Helpers;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using Web;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public abstract class ViewLogBaseController : BaseController
    {
        private readonly MicroSites microSites;
        private readonly IMediator mediator;

        protected ViewLogBaseController(
            IUserSession userSession,
            IMediator mediator,
            MicroSites microSites)
        {
            UserSession = userSession;
            this.mediator = mediator;
            this.microSites = microSites;
        }

        public IUserSession UserSession { get; set; }

        protected ActionResult View<TResponse>(IRequest<TResponse> message)
        {
            AssignDarkModeViewData(UserSession);
            return View(mediator.Send(message));
        }

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
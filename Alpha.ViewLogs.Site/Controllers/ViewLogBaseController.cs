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
            MicroSites microSites) : base(userSession)
        {
            this.mediator = mediator;
            this.microSites = microSites;
        }

        protected ActionResult View<TResponse>(IRequest<TResponse> message)
        {
            AssignDarkModeViewData();
            return View(mediator.Send(message));
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (userSession.IsSubAcc && !userSession.SubPageAccess.ViewLog)
            {
                Response.Redirect(microSites.OldMainPath(AppSettings.AccessDeniedPage));
            }
        }
    }
}
namespace Alpha.ViewLogs.Site.Controllers
{
    using MediatR;
    using Requests.CreditLogs;
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class CreditLogsController : BaseController
    {
        private readonly IMediator mediator;

        public CreditLogsController(IUserSession userSession, IMediator mediator)
        {
            CurrentUserSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            if (!CanViewSettingLogs(CurrentUserSession))
            {
                Response.Redirect(LoginLogsURL);
            }

            AssignServerSideParamForGetLogsQuery(indexQuery);

            var viewModel = mediator.Send(indexQuery);
            viewModel.ShowOldValuesCookieName = CookieShowOldValue;

            return View(viewModel);
        }
    }
}
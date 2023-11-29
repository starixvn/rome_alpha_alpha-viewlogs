namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using AlphaPlus.User.Models;
    using Helpers;
    using MediatR;
    using Models.Enumerations;
    using Requests.Login;

    public class LoginController : ViewLogBaseController
    {
        private readonly IMediator mediator;

        public LoginController(
            IUserSession userSession,
            IMediator mediator,
            MicroSites microSites) : base(userSession, microSites)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.IsAgentViewing = UserSession.RoleId == UserLevel.Agent.Value;
            indexQuery.PreviousUsername = UserSession.UserName;
            indexQuery.Username = indexQuery.IsAgentViewing && string.IsNullOrWhiteSpace(indexQuery.Username)
                ? UserSession.UserName
                : indexQuery.Username;
            indexQuery.IsInternal = UserSession.IsInternal;

            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
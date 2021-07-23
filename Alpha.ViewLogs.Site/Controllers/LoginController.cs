namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using Helpers;
    using MediatR;
    using Models.Enumerations;
    using Requests.Login;
    using Sunplus.Agent.Model.Entities;

    public class LoginController : ViewLogBaseController
    {
        public LoginController(
            IUserSession userSession,
            IMediator mediator,
            MicroSites microSites) : base(userSession, mediator, microSites)
        {
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.IsAgentViewing = UserSession.RoleId == UserLevel.Agent.Value;
            indexQuery.PreviousUsername = UserSession.UserName;
            indexQuery.Username = indexQuery.IsAgentViewing && string.IsNullOrWhiteSpace(indexQuery.Username)
                ? UserSession.UserName
                : indexQuery.Username;
            indexQuery.IsInternal = UserSession.IsInternal;

            return View(indexQuery);
        }
    }
}
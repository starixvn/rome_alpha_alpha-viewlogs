namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Models.Enumerations;
    using Requests.Login;
    using Sunplus.Agent.Model.Entities;

    public class LoginController : BaseController
    {
        private readonly IMediator mediator;
        private IUserSession currentUserSession;

        public LoginController(IUserSession currentUserSession, IMediator mediator)
        {
            this.mediator = mediator;
            this.currentUserSession = currentUserSession;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.IsAgentViewing = currentUserSession.RoleId == UserLevel.Agent.Value;
            indexQuery.Username = indexQuery.IsAgentViewing ? currentUserSession.UserName : indexQuery.Username;
            indexQuery.IsInternal = currentUserSession.IsInternal;

            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Models.Enumerations;
    using Requests.Login;

    public class LoginController : BaseController
    {
        private readonly IMediator mediator;

        public LoginController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            indexQuery.IsAgentViewing = UserSession.RoleId == UserLevel.Agent.Value;
            indexQuery.PreviousUsername = indexQuery.Username;
            indexQuery.Username = indexQuery.IsAgentViewing ? UserSession.UserName : indexQuery.Username;
            indexQuery.IsInternal = UserSession.IsInternal;

            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
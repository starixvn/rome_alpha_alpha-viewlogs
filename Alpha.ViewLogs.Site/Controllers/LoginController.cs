namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Requests.Login;
    using Sunplus.Agent.Model.Entities;

    public class LoginController : BaseController
    {
        private readonly IMediator mediator;

        public LoginController(IUserSession userSession, IMediator mediator)
        {
            CurrentUserSession = userSession;
            this.mediator = mediator;
        }

        public ActionResult Index(IndexQuery indexQuery)
        {
            var viewModel = mediator.Send(indexQuery);

            return View(viewModel);
        }
    }
}
namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using Helpers;
    using MediatR;
    using Requests.Credit;
    using Sunplus.Agent.Model.Entities;

    public class CreditController : ViewLogBaseController
    {
        public CreditController(
            IUserSession userSession,
            IMediator mediator,
            MicroSites microSites) : base(userSession, mediator, microSites)
        {
        }

        public ActionResult Index(IndexQuery indexQuery) => View(indexQuery);
    }
}
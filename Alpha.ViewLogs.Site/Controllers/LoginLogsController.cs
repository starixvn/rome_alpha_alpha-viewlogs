namespace Alpha.ViewLogs.Site.Controllers
{
    using Configurations;
    using MediatR;
    using Models.Enumerations;
    using Requests;
    using Requests.LoginLogs;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Model.Entities.Permission;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class LoginLogsController : BaseController
    {
        private readonly IMediator mediator;

        public LoginLogsController(IUserSession userSession, IMediator mediator)
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

        protected override void AssignServerSideParamForGetLogsQuery(BaseQuery getLogsQuery)
        {
            var isAgent = CurrentUserSession.RoleId == UserLevel.Agent.Value;

            getLogsQuery.RoleId = CurrentUserSession.RoleId;
            getLogsQuery.Username = isAgent ? CurrentUserSession.UserName : getLogsQuery.Username;

            getLogsQuery.HasViewLogPermission = CurrentUserSession.SubPageAccess != null && CurrentUserSession.SubPageAccess.ViewLog;
            getLogsQuery.Language = CurrentUserSession.Language;
            getLogsQuery.IsInternal = CurrentUserSession.IsInternal;
            getLogsQuery.IsShowOldValue = GetShowOldValue();
            getLogsQuery.UplineId = CurrentUserSession.CustId;
            getLogsQuery.NewFormatActionLogIds = AppSettings.NewFormatActionLogIds;
            getLogsQuery.IsShowMyanmarOdds = UserPermission.IsShowMyanmarOdds;
        }
    }
}
namespace Alpha.ViewLogs.Site.Controllers
{
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class BaseViewLogController : BaseController
    {
        public const string LoginLogsURL = "~/LoginLogs";

        public bool CanViewSettingLogs(IUserSession userSession)
        {
            return userSession.SubAccId <= 0 ||
                (userSession.SubPageAccess != null && userSession.SubPageAccess.ViewLog);
        }
    }
}
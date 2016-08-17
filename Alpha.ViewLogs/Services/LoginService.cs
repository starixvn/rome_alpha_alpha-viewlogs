namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models.Login;
    using Models;
    using Params.Login;

    public interface LoginService
    {
        PagingLogs<LoginLog> GetLogs(GetLogsParam getLogsParam);
    }
}

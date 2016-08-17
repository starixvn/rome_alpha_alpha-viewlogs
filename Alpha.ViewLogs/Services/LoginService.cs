namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params;

    public interface LoginService
    {
        PagingLogs<Log> GetLogs(GetLogsParam baseParam);
    }
}

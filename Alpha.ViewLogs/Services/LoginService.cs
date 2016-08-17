namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params;

    public interface LoginService
    {
        PagingLogs<BaseLog> GetLogs(BaseGetLogsParam baseParam);
    }
}

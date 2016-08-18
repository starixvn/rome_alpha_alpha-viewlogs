namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params;

    public interface StatusService
    {
        PagingLogs<Log> GetLogs(LogParam baseParam);
    }
}

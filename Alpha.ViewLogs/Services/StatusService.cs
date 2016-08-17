namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params.Status;

    public interface StatusService
    {
        PagingLogs<BaseLog> GetLogs(GetLogsParam getLogsParam);
    }
}

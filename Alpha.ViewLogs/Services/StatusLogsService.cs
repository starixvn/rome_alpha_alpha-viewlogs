namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params.StatusLogs;

    public interface StatusLogsService
    {
        PagingLogs GetLogs(GetLogsParam getLogsParam);
    }
}

namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params.CreditLogs;

    public interface CreditLogsService
    {
        PagingLogs GetLogs(GetLogsParam getLogsParam);
    }
}

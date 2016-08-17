namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params.Credit;

    public interface CreditService
    {
        PagingLogs<BaseLog> GetLogs(GetLogsParam getLogsParam);
    }
}

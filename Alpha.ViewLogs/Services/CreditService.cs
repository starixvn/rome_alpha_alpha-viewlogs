namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params;

    public interface CreditService
    {
        PagingLogs<BaseLog> GetLogs(BaseGetLogsParam baseParam);
    }
}

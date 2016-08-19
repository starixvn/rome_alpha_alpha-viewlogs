namespace Alpha.ViewLogs.Services
{
    using System.Collections.Generic;
    using Alpha.ViewLogs.Models;
    using Params;

    public interface LogService
    {
        IEnumerable<LogActionType> GetLogActionTypes();

        PagingLogs<Log> GetPagingLogs(LogParam logParam);
    }
}
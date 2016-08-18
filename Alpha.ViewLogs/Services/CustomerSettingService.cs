namespace Alpha.ViewLogs.Services
{
    using System.Collections.Generic;
    using Alpha.ViewLogs.Models;
    using Params;

    public interface CustomerSettingService
    {
        List<LogActionType> GetLogActionTypes();

        PagingLogs<Log> GetLogs(LogParam baseParam);
    }
}

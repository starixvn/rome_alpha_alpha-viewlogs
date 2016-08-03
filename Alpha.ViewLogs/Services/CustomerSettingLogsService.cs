namespace Alpha.ViewLogs.Services
{
    using Models;
    using Models.CustomerSettingLogs;
    using Params.CustomerSettingLogs;
    using System.Collections.Generic;

    public interface CustomerSettingLogsService
    {
        List<LogActionType> GetLogActionTypes();

        PagingLogs GetLogs(GetLogsParam getLogsParam);
    }
}

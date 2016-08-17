namespace Alpha.ViewLogs.Services
{
    using Models;
    using Models.CustomerSetting;
    using Params.CustomerSetting;
    using System.Collections.Generic;

    public interface CustomerSettingService
    {
        List<LogActionType> GetLogActionTypes();

        PagingLogs<BaseLog> GetLogs(GetLogsParam getLogsParam);
    }
}

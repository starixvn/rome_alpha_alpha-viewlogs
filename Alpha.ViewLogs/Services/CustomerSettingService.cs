namespace Alpha.ViewLogs.Services
{
    using Models.CustomerSetting;
    using Params.CustomerSetting;
    using System.Collections.Generic;

    public interface CustomerSettingService
    {
        List<LogActionType> LogActionTypes();

        void GetLogs(GetLogsParam getLogsParam);
    }
}

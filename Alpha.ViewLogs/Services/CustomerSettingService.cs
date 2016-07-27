namespace Alpha.ViewLogs.Services
{
    using Models.CustomerSetting;
    using System.Collections.Generic;

    public interface CustomerSettingService
    {
        List<LogActionType> LogActionTypes();
    }
}

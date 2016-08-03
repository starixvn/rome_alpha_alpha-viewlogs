namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using System.Collections.Generic;

    public interface BaseLogService
    {
        string GetLogActionName(int actionId, string language);

        LogAction GetLogActionByActionId(int actionId);
    }
}

namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;

    public interface BaseService
    {
        string GetLogActionName(int actionId, string language);

        LogAction GetLogActionByActionId(int actionId);
    }
}

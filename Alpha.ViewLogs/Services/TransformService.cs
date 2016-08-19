namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params;

    public interface TransformService
    {
        string TransformTrackingValue(string trackingValue, int logActionId, TransformParam transformParam);
    }
}

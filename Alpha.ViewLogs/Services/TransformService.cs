namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Params;

    public interface TransformService
    {
        string TransformTrackingValueToHtml(string trackingValue, int logActionId, TransformParam transformParam);
    }
}

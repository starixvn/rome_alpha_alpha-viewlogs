namespace Alpha.ViewLogs.Params
{
    public class StatusParam : BaseGetLogsParam
    {
        public bool IsDebug { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }
    }
}

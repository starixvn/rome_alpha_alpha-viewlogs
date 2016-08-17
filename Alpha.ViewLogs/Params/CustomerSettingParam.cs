namespace Alpha.ViewLogs.Params
{
    public class CustomerSettingParam : BaseGetLogsParam
    {
        public bool IsDebug { get; set; }

        public int ActionTypeId { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }
    }
}

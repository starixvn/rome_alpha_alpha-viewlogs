namespace Alpha.ViewLogs.Params
{
    public class CustomerSettingParam : GetLogsParam
    {
        public int ActionTypeId { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }
    }
}

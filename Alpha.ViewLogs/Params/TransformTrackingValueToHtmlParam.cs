namespace Alpha.ViewLogs.Params
{
    using System.Collections.Generic;

    public class TransformTrackingValueToHtmlParam
    {
        public bool IsDebug { get; set; }

        public bool IsShowOldValue { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }

        public Dictionary<string, bool> SportBetTypeVisibilities { get; set; }

        public bool IsShowMyanmarOdds { get; set; }
    }
}

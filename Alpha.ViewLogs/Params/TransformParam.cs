namespace Alpha.ViewLogs.Params
{
    using System.Collections.Generic;

    public class TransformParam
    {
        public bool IsShowOldValue { get; set; }

        public string TrackingValueTransformTemplatePath { get; set; }

        public Dictionary<string, bool> SportBetTypeVisibilities { get; set; }

        public bool IsShowMyanmarOdds { get; set; }
    }
}

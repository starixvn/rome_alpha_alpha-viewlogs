namespace Alpha.ViewLogs.Models
{
    public class IPRange
    {
        public IPRange(long from, long to, bool isManual)
        {
            From = from;
            To = to;
            IsManual = isManual;
        }

        public long From { get; }

        public long To { get; }

        public bool IsManual { get; }
    }
}

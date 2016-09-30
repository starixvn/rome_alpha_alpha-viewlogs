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

        public long From { get; private set; }

        public long To { get; private set; }

        public bool IsManual { get; private set; }
    }
}

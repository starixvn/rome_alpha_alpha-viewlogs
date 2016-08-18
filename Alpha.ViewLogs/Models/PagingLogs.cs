namespace Alpha.ViewLogs.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class PagingLogs<Log>
    {
        public PagingLogs()
        {
            Logs = Enumerable.Empty<Log>();
        }

        public IEnumerable<Log> Logs { get; set; }

        public int TotalRecords { get; set; }
    }
}

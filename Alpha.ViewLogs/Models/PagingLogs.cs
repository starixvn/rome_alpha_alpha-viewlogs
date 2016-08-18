namespace Alpha.ViewLogs.Models
{
    using System.Collections.Generic;

    public class PagingLogs<Log>
    {
        public List<Log> Logs { get; set; }

        public int TotalRecords { get; set; }
    }
}

namespace Alpha.ViewLogs.Models
{
    using System.Collections.Generic;

    public class PagingLogs
    {
        public List<BaseLog> Logs { get; set; }

        public int TotalRecords { get; set; }
    }
}

namespace Alpha.ViewLogs.Models.LoginLogs
{
    using System.Collections.Generic;

    public class PagingLoginLogs
    {
        public List<LoginLog> Logs { get; set; }

        public int TotalRecords { get; set; }
    }
}

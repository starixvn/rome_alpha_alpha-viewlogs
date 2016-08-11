namespace Alpha.ViewLogs.DataAccesses
{
    using Fanex.Data.Repository;
    using Models.LoginLogs;
    using System.Collections.Generic;

    public interface LoginLogsDataAccess
    {
        List<LoginLog> GetLogs(ICriteria criteria);
    }
}

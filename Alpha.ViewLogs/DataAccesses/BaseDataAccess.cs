namespace Alpha.ViewLogs.DataAccesses
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;

    public interface BaseDataAccess
    {
        int GetTotalRecordsOfLogs(ICriteria criteria);

        List<BaseLog> GetLogs<BaseLog>(ICriteria criteria);
    }
}

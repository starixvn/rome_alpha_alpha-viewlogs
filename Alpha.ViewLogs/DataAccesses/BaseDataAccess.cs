namespace Alpha.ViewLogs.DataAccesses
{
    using Fanex.Data.Repository;
    using System.Collections.Generic;

    public interface BaseDataAccess
    {
        int GetTotalRecordsOfLogs(ICriteria criteria);

        List<BaseLog> GetLogs<BaseLog>(ICriteria criteria);
    }
}

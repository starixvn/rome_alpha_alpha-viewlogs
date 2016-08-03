namespace Alpha.ViewLogs.DataAccesses
{
    using Fanex.Data.Repository;

    public interface BaseDataAccess
    {
        int GetTotalRecordsOfLogs(ICriteria criteria);
    }
}

namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class BaseDataAccessImpl : BaseDataAccess
    {
        public int GetTotalRecordsOfLogs(ICriteria criteria)
        {
            int totalRecords;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                totalRecords = Converter.ToInt(objectDb.ExecuteScalar(criteria.GetSpParams()));
            }

            return totalRecords;
        }
    }
}

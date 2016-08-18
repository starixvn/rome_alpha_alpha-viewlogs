namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Models;

    public class LogDataAccessImpl : LogDataAccess
    {
        private IDynamicRepository dynamicRepository;

        public LogDataAccessImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public int GetTotalRecordsOfLogs(ICriteria criteria)
        {
            int totalRecords;
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                totalRecords = Converter.ToInt(objectDb.ExecuteScalar(criteria.GetSpParams()));
            }

            return totalRecords;
        }

        public virtual List<Log> GetLogs(ICriteria criteria)
        {
            return dynamicRepository.Fetch<Log>(criteria).ToList();
        }
    }
}

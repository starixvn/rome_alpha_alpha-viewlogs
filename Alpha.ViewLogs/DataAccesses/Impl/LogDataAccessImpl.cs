namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using System.Collections.Generic;
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
            if (criteria == null || !criteria.IsValid())
            {
                return 0;
            }

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                return Converter.ToInt(objectDb.ExecuteScalar(criteria.GetSpParams()));
            }
        }

        public virtual IEnumerable<Log> GetLogs(ICriteria criteria)
        {
            if (criteria == null || !criteria.IsValid())
            {
                return new List<Log>();
            }

            return dynamicRepository.Fetch<Log>(criteria);
        }
    }
}

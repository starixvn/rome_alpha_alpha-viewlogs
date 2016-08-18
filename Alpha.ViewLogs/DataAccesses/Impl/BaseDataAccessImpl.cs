namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class BaseDataAccessImpl : BaseDataAccess
    {
        private IDynamicRepository dynamicRepository;

        public BaseDataAccessImpl(IDynamicRepository dynamicRepository)
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

        public virtual List<LogType> GetLogs<LogType>(ICriteria criteria)
        {
            return dynamicRepository.Fetch<LogType>(criteria).ToList();
        }
    }
}

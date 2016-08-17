namespace Alpha.ViewLogs.DataAccesses
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using Models;

    public interface LoginDataAccess
    {
        List<BaseLog> GetLogs(ICriteria criteria);
    }
}

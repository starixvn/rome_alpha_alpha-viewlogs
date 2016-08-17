namespace Alpha.ViewLogs.DataAccesses
{
    using Fanex.Data.Repository;
    using Models.Login;
    using System.Collections.Generic;

    public interface LoginDataAccess
    {
        List<LoginLog> GetLogs(ICriteria criteria);
    }
}

namespace Alpha.ViewLogs.DataAccesses
{
    using Alpha.ViewLogs.Models;
    using Criteria;

    public interface CustomerDataAccess
    {
        CustomerInfo GetCustInfoByUsername(GetCustInfoByUsernameCriteria criteria);
    }
}

namespace Alpha.ViewLogs.DataAccesses
{
    using Alpha.ViewLogs.Models;
    using Criteria;

    public interface CustomerDataAccess
    {
        CustomerInfo CheckIsBelongingByUsername(CheckIsBelongingByUsernameCriteria criteria);
    }
}

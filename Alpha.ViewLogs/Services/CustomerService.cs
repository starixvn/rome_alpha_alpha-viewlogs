namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models;
    using Criteria;

    public interface CustomerService
    {
        CustomerInfo GetDownlineInfoByUsername(GetCustInfoByUsernameCriteria criteria);
    }
}

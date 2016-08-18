namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using DataAccesses;
    using Models;

    public class CustomerServiceImpl : CustomerService
    {
        private readonly CustomerDataAccess customerDataAccess;

        public CustomerServiceImpl(CustomerDataAccess customerDataAccess)
        {
            this.customerDataAccess = customerDataAccess;
        }

        public CustomerInfo GetDownlineInfoByUsername(GetCustInfoByUsernameCriteria criteria)
        {
            if (string.IsNullOrEmpty(criteria.Username))
            {
                return new CustomerInfo();
            }
            
            var customerInfo = customerDataAccess.GetCustInfoByUsername(criteria);

            return customerInfo;
        }
    }
}

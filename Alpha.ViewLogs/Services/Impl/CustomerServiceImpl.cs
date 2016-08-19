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

        public CustomerInfo GetDownlineInfoByUsername(string downlineUsername, int uplineId)
        {
            if (string.IsNullOrEmpty(downlineUsername))
            {
                return new CustomerInfo();
            }
            
            var customerInfo = customerDataAccess.GetCustInfoByUsername(new GetCustInfoByUsernameCriteria
            {
                Username = downlineUsername,
                UplineId = uplineId
            });

            return customerInfo;
        }
    }
}

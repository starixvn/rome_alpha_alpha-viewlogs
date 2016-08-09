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

        public CustomerInfo CheckIsBelongingByUsername(CheckIsBelongingByUsernameCriteria criteria)
        {
            if (string.IsNullOrEmpty(criteria.Username))
            {
                return null;
            }

            var customerInfo = customerDataAccess.CheckIsBelongingByUsername(criteria);
            return customerInfo;
        }
    }
}

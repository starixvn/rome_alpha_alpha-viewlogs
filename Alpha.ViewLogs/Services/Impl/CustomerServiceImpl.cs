namespace Alpha.ViewLogs.Services.Impl
{
    using Criteria;
    using DataAccesses;
    using Fanex.Data.Repository;
    using Models;

    public class CustomerServiceImpl : CustomerService
    {
        private readonly CustomerDataAccess customerDataAccess;
        private readonly IDynamicRepository dynamicRepository;

        public CustomerServiceImpl(
            CustomerDataAccess customerDataAccess,
            IDynamicRepository dynamicRepository)
        {
            this.customerDataAccess = customerDataAccess;
            this.dynamicRepository = dynamicRepository;
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

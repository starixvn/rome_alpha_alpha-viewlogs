namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using Alpha.ViewLogs.Models;
    using Criteria;
    using Fanex.Data.Repository;

    public class CustomerDataAccessImpl : CustomerDataAccess
    {
        private IDynamicRepository dynamicRepository;

        public CustomerDataAccessImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public CustomerInfo GetCustInfoByUsername(GetCustInfoByUsernameCommand command)
        {
            var customerInfo = new CustomerInfo();

            dynamicRepository.Execute(command);

            customerInfo.CustId = command.CustId;
            customerInfo.IsSubAcc = command.IsSubAcc;

            return customerInfo;
        }
    }
}

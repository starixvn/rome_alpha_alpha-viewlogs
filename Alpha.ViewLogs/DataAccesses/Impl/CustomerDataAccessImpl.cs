namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using Alpha.ViewLogs.Models;
    using Criteria;
    using Fanex.Data;

    public class CustomerDataAccessImpl : CustomerDataAccess
    {
        public CustomerInfo GetCustInfoByUsername(GetCustInfoByUsernameCriteria criteria)
        {
            var customerInfo = new CustomerInfo();
            if (!criteria.IsValid())
            {
                return customerInfo;
            }

            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria);

                customerInfo.CustId = criteria.CustId;
                customerInfo.IsSubAcc = criteria.IsSubAcc;
            }

            return customerInfo;
        }
    }
}

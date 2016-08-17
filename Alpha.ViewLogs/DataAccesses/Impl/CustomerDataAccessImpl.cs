namespace Alpha.ViewLogs.DataAccesses.Impl
{
    using Alpha.ViewLogs.Models;
    using Criteria;
    using Fanex.Common;
    using Fanex.Data;

    public class CustomerDataAccessImpl : CustomerDataAccess
    {
        public CustomerInfo GetCustInfoByUsername(GetCustInfoByUsernameCriteria criteria)
        {
            var customerInfo = new CustomerInfo();
            using (var objectDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objectDb.ExecuteNonQuery(criteria.GetSpParams());

                var outputParams = objectDb.GetOuputParams();
                customerInfo.CustId = Converter.ToInt(outputParams["custid"]);
                customerInfo.IsSubAcc = Converter.ToBool(outputParams["issubacc"]);
            }

            return customerInfo;
        }
    }
}

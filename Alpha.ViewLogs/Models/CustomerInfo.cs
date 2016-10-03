namespace Alpha.ViewLogs.Models
{
    public class CustomerInfo
    {
        public CustomerInfo(int custId, bool isSubAcc)
        {
            CustId = custId;
            IsSubAcc = isSubAcc;
        }

        public int CustId { get; private set; }

        public bool IsSubAcc { get; private set; }
    }
}

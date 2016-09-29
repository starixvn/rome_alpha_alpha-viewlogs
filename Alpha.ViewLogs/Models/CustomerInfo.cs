namespace Alpha.ViewLogs.Models
{
    using System.Text;

    public class CustomerInfo
    {
        public CustomerInfo(
            int custId,
            bool isSubAcc)
        {
            CustId = custId;
            IsSubAcc = isSubAcc;
        }

        public int CustId { get; private set; }

        public bool IsSubAcc { get; private set; }

        /// <summary>
        /// Equals: used for compare two object (Unit test)
        /// </summary>
        public override bool Equals(object obj)
        {
            var compareObject = (CustomerInfo)obj;

            var isEqual = CustId == compareObject.CustId &&
                IsSubAcc == compareObject.IsSubAcc;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return CustId.GetHashCode() ^ IsSubAcc.GetHashCode();
        }

        /// <summary>
        /// ToString: use for displaying compare result two object (Unit test)
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("CustId: " + CustId);
            result.AppendLine("IsSubAcc: " + IsSubAcc);

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}

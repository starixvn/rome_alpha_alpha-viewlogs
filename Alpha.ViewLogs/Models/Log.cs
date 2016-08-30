namespace Alpha.ViewLogs.Models
{
    using System;
    using System.Text;

    public class Log
    {
        public int ActionId { get; set; }

        public string ActionName { get; set; }

        public int ActionTypeId { get; set; }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte IsInternal { get; set; }

        public DateTime LogDate { get; set; }

        public long LogId { get; set; }

        public string SPName { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string TrackingValue { get; set; }

        public string TrackingValueOld { get; set; }

        public string IP { get; set; }

        public string Details { get; set; }

        /// <summary>
        /// Equals: used for compare two object (Unit test)
        /// </summary>
        public override bool Equals(object obj)
        {
            var compareViewModel = (Log)obj;

            var isEqual = ActionId == compareViewModel.ActionId &&
                ActionName == compareViewModel.ActionName &&
                ActionTypeId == compareViewModel.ActionTypeId &&
                CustId == compareViewModel.CustId &&
                CustName == compareViewModel.CustName &&
                IsInternal == compareViewModel.IsInternal &&
                LogDate == compareViewModel.LogDate &&
                LogId == compareViewModel.LogId &&
                SPName == compareViewModel.SPName &&
                UserId == compareViewModel.UserId &&
                Username == compareViewModel.Username &&
                TrackingValue == compareViewModel.TrackingValue &&
                TrackingValueOld == compareViewModel.TrackingValueOld &&
                IP == compareViewModel.IP &&
                Details == compareViewModel.Details;

            return isEqual;
        }

        public override int GetHashCode()
        {
            return ActionId.GetHashCode() ^
                ActionName.GetHashCode() ^
                ActionTypeId.GetHashCode() ^
                CustId.GetHashCode() ^
                CustName.GetHashCode() ^
                IsInternal.GetHashCode() ^
                LogDate.GetHashCode() ^
                LogId.GetHashCode() ^
                SPName.GetHashCode() ^
                UserId.GetHashCode() ^
                Username.GetHashCode() ^
                TrackingValue.GetHashCode() ^
                TrackingValueOld.GetHashCode() ^
                IP.GetHashCode() ^
                Details.GetHashCode();
        }

        /// <summary>
        /// ToString: use for displaying compare result two object (Unit test)
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("ActionId: " + ActionId);
            result.AppendLine("ActionName: " + ActionName);
            result.AppendLine("ActionTypeId: " + ActionTypeId);
            result.AppendLine("CustId: " + CustId);
            result.AppendLine("CustName: " + CustName);
            result.AppendLine("IsInternal: " + IsInternal);
            result.AppendLine("LogDate: " + LogDate);
            result.AppendLine("LogId: " + LogId);
            result.AppendLine("SPName: " + SPName);
            result.AppendLine("UserId: " + UserId);
            result.AppendLine("Username: " + Username);
            result.AppendLine("TrackingValue: " + TrackingValue);
            result.AppendLine("TrackingValueOld: " + TrackingValueOld);
            result.AppendLine("IP: " + IP);
            result.AppendLine("Details: " + Details);

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}

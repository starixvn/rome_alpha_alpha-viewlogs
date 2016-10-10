namespace Alpha.ViewLogs.Site.Tests.ViewModelsForTesting.Login
{
    using System.Text;
    using ViewModels.Login;

    public class LoginLogItemViewModelForTesting : LoginLogItemViewModel
    {
        public static LoginLogItemViewModelForTesting FromLoginLogItemViewModel(LoginLogItemViewModel logItemViewModel)
        {
            if (logItemViewModel == null)
            {
                return null;
            }

            return new LoginLogItemViewModelForTesting
            {
                ActionId = logItemViewModel.ActionId,
                ActionName = logItemViewModel.ActionName,
                ActionTypeId = logItemViewModel.ActionTypeId,
                CustId = logItemViewModel.CustId,
                CustName = logItemViewModel.CustName,
                IsInternal = logItemViewModel.IsInternal,
                LogDate = logItemViewModel.LogDate,
                LogId = logItemViewModel.LogId,
                SPName = logItemViewModel.SPName,
                UserId = logItemViewModel.UserId,
                Username = logItemViewModel.Username,
                TrackingValue = logItemViewModel.TrackingValue,
                TrackingValueOld = logItemViewModel.TrackingValueOld,
                Details = logItemViewModel.Details,
                IP = logItemViewModel.IP,
                Domain = logItemViewModel.Domain,
                Browser = logItemViewModel.Browser,
                Country = logItemViewModel.Country
            };
        }

        public override bool Equals(object obj)
        {
            var compareObject = (LoginLogItemViewModel)obj;

            return ActionId == compareObject.ActionId
                && ActionName == compareObject.ActionName
                && ActionTypeId == compareObject.ActionTypeId
                && CustId == compareObject.CustId
                && CustName == compareObject.CustName
                && IsInternal == compareObject.IsInternal
                && LogDate == compareObject.LogDate
                && LogId == compareObject.LogId
                && SPName == compareObject.SPName
                && UserId == compareObject.UserId
                && Username == compareObject.Username
                && TrackingValue == compareObject.TrackingValue
                && TrackingValueOld == compareObject.TrackingValueOld
                && Details == compareObject.Details
                && IP == compareObject.IP
                && Domain == compareObject.Domain
                && Browser == compareObject.Browser
                && Country == compareObject.Country;
        }

        public override int GetHashCode()
        {
            return ActionId.GetHashCode()
                ^ ActionName.GetHashCode()
                ^ ActionTypeId.GetHashCode()
                ^ CustId.GetHashCode()
                ^ CustName.GetHashCode()
                ^ IsInternal.GetHashCode()
                ^ LogDate.GetHashCode()
                ^ ActionTypeId.GetHashCode()
                ^ LogId.GetHashCode()
                ^ SPName.GetHashCode()
                ^ UserId.GetHashCode()
                ^ Username.GetHashCode()
                ^ TrackingValue.GetHashCode()
                ^ TrackingValueOld.GetHashCode()
                ^ Details.GetHashCode()
                ^ IP.GetHashCode()
                ^ Domain.GetHashCode()
                ^ Browser.GetHashCode()
                ^ Country.GetHashCode();
        }

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
            result.AppendLine("Details: " + Details);
            result.AppendLine("IP: " + IP);
            result.AppendLine("Domain: " + Domain);
            result.AppendLine("Browser: " + Browser);
            result.AppendLine("Country: " + Country);

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}

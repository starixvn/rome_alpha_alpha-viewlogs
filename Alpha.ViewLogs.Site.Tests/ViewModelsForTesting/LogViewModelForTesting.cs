namespace Alpha.ViewLogs.Site.Tests.ViewModelsForTesting
{
    using System.Linq;
    using System.Text;
    using Models;
    using Params;
    using ViewModels;

    public class LogViewModelForTesting : LogViewModel
    {
        public LogViewModelForTesting(LogParam logParam, int totalRecords)
        {
        }

        public static LogViewModelForTesting FromLog(LogViewModel logViewModel)
        {
            if (logViewModel == null)
            {
                return null;
            }

            return new LogViewModelForTesting
            {
                TotalRecords = pagingLogs.TotalRecords,
                Logs = pagingLogs.Logs
            };
        }

        public override bool Equals(object obj)
        {
            var compareObject = (PagingLogs<Log>)obj;

            var isEqual = true;
            foreach (var log in Logs)
            {
                var compareLog = compareObject.Logs.Where(l => l.LogId == log.LogId).First();
                if (compareLog == null || !compareLog.Equals(log))
                {
                    isEqual = false;
                    break;
                }
            }

            isEqual &= TotalRecords == compareObject.TotalRecords;
            return isEqual;
        }

        public override int GetHashCode()
        {
            return HasViewLogPermission.GetHashCode()
                ^ IsSubAccount.GetHashCode()
                ^ RoleId.GetHashCode()
                ^ UserLevel.GetHashCode()
                ^ ShowOldValuesCookieName.GetHashCode()
                ^ UsernameAutoSuggestionPath.GetHashCode()
                ^ ViewIPInfoPath.GetHashCode()
                ^ ActionTypeId.GetHashCode()
                ^ HasLogs.GetHashCode()
                ^ ShowOldValue.GetHashCode()
                ^ TotalRecords.GetHashCode()
                ^ PageSize.GetHashCode()
                ^ PageIndex.GetHashCode()
                ^ LogItems.GetHashCode()
                ^ DayInMonth.GetHashCode()
                ^ FromDate.GetHashCode()
                ^ ToDate.GetHashCode()
                ^ MinReportDate.GetHashCode()
                ^ MaxReportDate.GetHashCode()
                ^ Username.GetHashCode();
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine("HasViewLogPermission: " + HasViewLogPermission);
            result.AppendLine("IsSubAccount: " + IsSubAccount);
            result.AppendLine("RoleId: " + RoleId);
            result.AppendLine("UserLevel: " + UserLevel);
            result.AppendLine("ShowOldValuesCookieName: " + ShowOldValuesCookieName);
            result.AppendLine("UsernameAutoSuggestionPath: " + UsernameAutoSuggestionPath);
            result.AppendLine("ViewIPInfoPath: " + ViewIPInfoPath);
            result.AppendLine("ActionTypeId: " + ActionTypeId);
            result.AppendLine("HasLogs: " + HasLogs);
            result.AppendLine("ShowOldValue: " + ShowOldValue);
            result.AppendLine("TotalRecords: " + TotalRecords);
            result.AppendLine("PageSize: " + PageSize);
            result.AppendLine("PageIndex: " + PageIndex);
            result.AppendLine("DayInMonth: " + DayInMonth);
            result.AppendLine("FromDate: " + FromDate);
            result.AppendLine("ToDate: " + ToDate);
            result.AppendLine("MinReportDate: " + MinReportDate);
            result.AppendLine("MaxReportDate: " + MaxReportDate);
            result.AppendLine("Username: " + Username);

            foreach (var log in LogItems)
            {
                result.AppendLine("Log: " + log);
            }

            result.AppendLine(base.ToString());
            return result.ToString();
        }
    }
}

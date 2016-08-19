namespace Alpha.ViewLogs.Site.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using Models.Enumerations;

    public class LogViewModel
    {
        public bool HasViewLogPermission { get; set; }

        public bool IsSubAccount { get; set; }

        public byte RoleId { get; set; }

        public string UserLevel
        {
            get
            {
                return Enumeration.FromValue<UserLevel>(RoleId).DisplayName.ToLower();
            }
        }

        public string ShowOldValuesCookieName { get; set; }

        public string UsernameAutoSuggestionPath
        {
            get
            {
                return MicroSites.OldMainPath("~/_GlobalResources/Handlers/QueryUserName.ashx");
            }
        }

        public string ViewIPInfoPath
        {
            get
            {
                return MicroSites.OldMainPath("~/_IPInfo/IpInfo.aspx");
            }
        }

        public string ActionTypeId { get; set; }

        public bool HasLogs
        {
            get
            {
                return LogItems != null && LogItems.Any();
            }
        }

        public bool ShowOldValue { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public List<LogItemViewModel> LogItems { get; set; }

        public string DayInMonth { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string MinReportDate { get; set; }

        public string MaxReportDate { get; set; }

        public string Username { get; set; }
    }
}
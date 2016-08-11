namespace Alpha.ViewLogs.Site.ViewModels.CreditLogs
{
    using Alpha.ViewLogs.Params.CreditLogs;
    using Constants;
    using Models.Enumerations;
    using System.Collections.Generic;
    using ViewLogs.Helpers;

    public class IndexViewModel
    {
        private readonly bool hasViewLogPermission;
        private readonly bool isSubAccount;
        private readonly byte roleId;

        public IndexViewModel(GetLogsParam logsParam)
        {
            this.hasViewLogPermission = logsParam.HasViewLogPermission;
            this.isSubAccount = logsParam.SubAccId > 0;
            this.roleId = logsParam.RoleId;
        }

        public bool HasViewLogPermission
        {
            get
            {
                return hasViewLogPermission;
            }
        }

        public bool IsSubAccount
        {
            get
            {
                return isSubAccount;
            }
        }

        public byte RoleId
        {
            get
            {
                return this.roleId;
            }
        }

        public string UserLevel
        {
            get
            {
                return Enumeration.FromValue<UserLevel>(this.roleId).DisplayName.ToLower();
            }
        }

        public string SelectedTab
        {
            get
            {
                return ViewLogType.Credit;
            }
        }

        public List<TabViewModal> Tabs { get; set; }

        public GetLogsViewModel GetLogsViewModel { get; set; }

        public string ShowOldValuesCookieName { get; set; }

        public string IndexPath
        {
            get
            {
                return MicroSites.MainUrl("~/creditlogs/index");
            }
        }

        public string UsernameAutoSuggestionPath
        {
            get
            {
                return MicroSites.OldMainUrl("~/_GlobalResources/Handlers/QueryUserName.ashx");
            }
        }

        public string ActionTypeId { get; set; }
    }
}
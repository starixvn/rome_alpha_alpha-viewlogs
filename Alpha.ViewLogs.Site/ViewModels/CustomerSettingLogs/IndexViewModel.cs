namespace Alpha.ViewLogs.Site.ViewModels.CustomerSettingLogs
{
    using Models.Enumerations;
    using Params.CustomerSettingLogs;
    using System.Collections.Generic;

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

        public List<LogActionTypeViewModel> LogActionTypes { get; set; }

        public string SelectedTab { get; set; }

        public List<ElementViewModal> Tabs { get; set; }

        public GetLogsViewModel GetLogsViewModel { get; set; }

        public string ShowOldValuesCookieName { get; set; }
    }
}
namespace Alpha.ViewLogs.Site.ViewModels.CustomerSettingLogs
{
    using Models;
    using Models.Enumerations;
    using Requests;
    using System.Collections.Generic;

    public class CustomerSettingLogsViewModel
    {
        private readonly byte roleId;
        private readonly bool hasViewLogPermission;
        private readonly bool isSubAccount;
        private readonly int totalRecords;

        public CustomerSettingLogsViewModel(
            CustomerSettingLogsQuery customerSettingQuery,
            PagingLogs pagingLogs)
        {
            this.roleId = customerSettingQuery.RoleId;
            this.hasViewLogPermission = customerSettingQuery.HasViewLogPermission;
            this.isSubAccount = customerSettingQuery.SubAccId > 0;

            this.totalRecords = pagingLogs.TotalRecords;
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

        public int TotalRecords
        {
            get
            {
                return totalRecords;
            }
        }

        public List<LogActionTypeViewModel> LogActionTypes { get; set; }

        public List<BaseLogViewModel> Logs { get; set; }
    }
}
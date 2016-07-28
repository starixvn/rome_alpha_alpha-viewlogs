namespace Alpha.ViewLogs.Site.ViewModels.CustomerSetting
{
    using Models.Enumerations;
    using Requests;
    using System.Collections.Generic;

    public class CustomerSettingViewModel
    {
        private readonly byte roleId;
        private readonly bool hasViewLogPermission;
        private readonly bool isSubAccount;

        public CustomerSettingViewModel(CustomerSettingQuery customerSettingQuery)
        {
            this.roleId = customerSettingQuery.RoleId;
            this.hasViewLogPermission = customerSettingQuery.HasViewLogPermission;
            this.isSubAccount = customerSettingQuery.SubAccId > 0;
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

        public List<LogActionTypeViewModel> LogActionTypes { get; set; }
    }
}
namespace Alpha.ViewLogs.Site.ViewModels.CustomerSetting
{
    using Models.CustomerSetting;
    using Models.Enumerations;
    using Requests;
    using System.Collections.Generic;

    public class CustomerSettingViewModel
    {
        private readonly byte roleId;

        public CustomerSettingViewModel(CustomerSettingQuery customerSettingQuery)
        {
            this.roleId = customerSettingQuery.RoleId;
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

        public List<LogActionType> LogActionTypes { get; set; }
    }
}
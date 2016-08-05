namespace Alpha.ViewLogs.Site.Handlers
{
    using Alpha.ViewLogs.Site.ViewModels;
    using AppResources;
    using Constants;
    using System.Collections.Generic;

    public class BaseViewLogsHandler
    {
        public List<ElementViewModal> GetTabs(bool isSubAccount, bool hasViewLogPermission)
        {
            var tabs = new List<ElementViewModal>();
            var canViewSettingLogs = !isSubAccount || hasViewLogPermission;

            if (canViewSettingLogs)
            {
                tabs.Add(new ElementViewModal()
                {
                    Id = ViewLogType.CustomerSetting,
                    Name = Resources.CustomerSetting
                });

                tabs.Add(new ElementViewModal()
                {
                    Id = ViewLogType.Status,
                    Name = Resources.Status
                });

                tabs.Add(new ElementViewModal()
                {
                    Id = ViewLogType.Credit,
                    Name = Resources.Credit
                });
            }

            tabs.Add(new ElementViewModal()
            {
                Id = ViewLogType.Login,
                Name = Resources.Login
            });

            return tabs;
        }
    }
}
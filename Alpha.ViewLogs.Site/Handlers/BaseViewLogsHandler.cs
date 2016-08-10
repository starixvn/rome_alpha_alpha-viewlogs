namespace Alpha.ViewLogs.Site.Handlers
{
    using Alpha.ViewLogs.Site.ViewModels;
    using AppResources;
    using Constants;
    using System.Collections.Generic;
    using ViewLogs.Helpers;

    public class BaseViewLogsHandler
    {
        public List<TabViewModal> GetTabs(bool isSubAccount, bool hasViewLogPermission)
        {
            var tabs = new List<TabViewModal>();
            var canViewSettingLogs = !isSubAccount || hasViewLogPermission;

            if (canViewSettingLogs)
            {
                tabs.Add(new TabViewModal()
                {
                    Id = ViewLogType.CustomerSetting,
                    Name = Resources.CustomerSetting,
                    HRef = MicroSites.MainUrl("~/customersettinglogs/index")
                });

                tabs.Add(new TabViewModal()
                {
                    Id = ViewLogType.Status,
                    Name = Resources.Status,
                    HRef = MicroSites.MainUrl("~/statuslogs/index")
                });

                tabs.Add(new TabViewModal()
                {
                    Id = ViewLogType.Credit,
                    Name = Resources.Credit,
                    HRef = MicroSites.MainUrl("~/creditlogs/index")
                });
            }

            tabs.Add(new TabViewModal()
            {
                Id = ViewLogType.Login,
                Name = Resources.Login,
                HRef = MicroSites.MainUrl("~/loginlogs/index")
            });

            return tabs;
        }
    }
}
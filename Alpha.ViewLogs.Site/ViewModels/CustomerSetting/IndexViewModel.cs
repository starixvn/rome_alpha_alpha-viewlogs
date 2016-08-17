namespace Alpha.ViewLogs.Site.ViewModels.CustomerSetting
{
    using System.Collections.Generic;
    using Params;
    using ViewLogs.Helpers;

    public class IndexViewModel : BaseIndexViewModel
    {
        public IndexViewModel(GetLogsParam logsParam) : base(logsParam)
        {
        }

        public List<LogActionTypeViewModel> LogActionTypes { get; set; }

        public override string IndexPath
        {
            get
            {
                return MicroSites.ViewLogsPath("~/customersetting/index");
            }
        }
    }
}
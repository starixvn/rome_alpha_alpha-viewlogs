namespace Alpha.ViewLogs.Site.ViewModels.Status
{
    using Params;
    using ViewLogs.Helpers;

    public class IndexViewModel : BaseIndexViewModel
    {
        public IndexViewModel(GetLogsParam logsParam) : base(logsParam)
        {
        }

        public override string IndexPath
        {
            get
            {
                return MicroSites.ViewLogsPath("~/status/index");
            }
        }
    }
}
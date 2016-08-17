namespace Alpha.ViewLogs.Site.ViewModels.Login
{
    using Params;
    using ViewLogs.Helpers;

    public class IndexViewModel : BaseIndexViewModel
    {
        public IndexViewModel(GetLogsParam logsParam) : base(logsParam)
        {
        }

        public string PreviousUsername { get; set; }

        public override string IndexPath
        {
            get
            {
                return MicroSites.ViewLogsPath("~/login/index");
            }
        }
    }
}
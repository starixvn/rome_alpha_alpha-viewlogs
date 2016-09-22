namespace Alpha.ViewLogs.Site.ViewModels.Login
{
    using Alpha.ViewLogs.Params;

    public class IndexViewModel : LogViewModel
    {
        public IndexViewModel(LogParam logParam, int totalRecords) : base(logParam, totalRecords)
        {
        }

        public string PreviousUsername { get; set; }
    }
}
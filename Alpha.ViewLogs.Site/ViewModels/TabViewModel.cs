namespace Alpha.ViewLogs.Site.ViewModels
{
    public class TabViewModel
    {
        private string _selectedTabName;

        public TabViewModel(string selectedTabName)
        {
            _selectedTabName = selectedTabName;
        }

        public string SelectedTabName
        {
            get
            {
                return _selectedTabName.ToLower();
            }
        }
    }
}
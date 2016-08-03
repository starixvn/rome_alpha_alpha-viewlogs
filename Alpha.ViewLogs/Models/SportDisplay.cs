namespace Alpha.ViewLogs.Models
{
    public class SportDisplay
    {
        public SportDisplay(string sportId, bool isVisible, string displayName, string displayValue)
        {
            SportId = sportId;
            IsVisible = isVisible;
            DisplayName = displayName;
            DisplayValue = displayValue;
        }

        public string SportId { get; set; }

        public bool IsVisible { get; set; }

        public string DisplayName { get; set; }

        public string DisplayValue { get; set; }
    }
}

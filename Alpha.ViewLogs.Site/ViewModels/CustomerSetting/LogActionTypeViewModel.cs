namespace Alpha.ViewLogs.Site.ViewModels.CustomerSetting
{
    using Alpha.ViewLogs.Models.CustomerSetting;
    using ViewLogs.Constants;

    public class LogActionTypeViewModel
    {
        public LogActionTypeViewModel(LogActionType logActionType, string language)
        {
            this.Id = logActionType.Id;
            this.Name = GetName(logActionType, language);
        }

        public LogActionTypeViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        private string GetName(LogActionType logActionType, string language)
        {
            switch (language)
            {
                case Language.English:
                    return logActionType.EnglishName;
                case Language.ChineseTraditional:
                    return logActionType.ChineseTraditionalName;
                case Language.ChineseSimplified:
                    return logActionType.ChineseSimplifiedName;
                case Language.Japanese:
                    return logActionType.JapaneseName;
                case Language.Thai:
                    return logActionType.ThaiName;
                case Language.Korean:
                    return logActionType.KoreanName;
                case Language.Vietnamese:
                    return logActionType.VietnameseName;
                default:
                    return logActionType.EnglishName;
            }
        }
    }
}
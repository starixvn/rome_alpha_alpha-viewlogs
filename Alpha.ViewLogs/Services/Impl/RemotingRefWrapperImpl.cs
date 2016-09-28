namespace Alpha.ViewLogs.Services.Impl
{
    using System.Threading;
    using Constants;
    using RemotingRef;

    public class RemotingRefWrapperImpl : RemotingRefWrapper
    {
        private string CurrentLanguageCode
        {
            get
            {
                var currentLanguage = Thread.CurrentThread.CurrentCulture.Name;
                return ShortLanguageCode.CodeFromCurrentLanguageMapping.ContainsKey(currentLanguage)
                    ? ShortLanguageCode.CodeFromCurrentLanguageMapping[currentLanguage]
                    : ShortLanguageCode.Default;
            }
        }

        public string GetBetType(object betTypeId, object betId = null)
        {
            // resourceId remove first character
            string resourceId = string.Empty;

            if (betId != null && betId.ToString().Length > 1)
            {
                resourceId = betId.ToString().Substring(1);
            }

            return betTypeId == null ?
                string.Empty :
                RefData.GetBetTypeName(betTypeId.ToString(), CurrentLanguageCode, string.Empty, resourceId);
        }

        public string GetBetTypeName(object bettypeId)
        {
            return bettypeId != null
                ? RefData.GetBetTypeName(bettypeId.ToString(), CurrentLanguageCode)
                : string.Empty;
        }

        public string GetSportName(object sportId)
        {
            return sportId == null ?
                string.Empty :
                RefData.GetSportName(sportId.ToString(), CurrentLanguageCode, string.Empty);
        }

        public string GetProductName(int productId)
        {
            return RefData.GetResourceDataByRefIdAndResourceId("Product", productId.ToString(), CurrentLanguageCode, string.Empty);
        }
    }
}

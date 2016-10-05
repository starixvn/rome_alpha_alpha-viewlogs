namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using System.Threading;
    using Constants;
    using RemotingRef;

    public class RemotingRefWrapperImpl : RemotingRefWrapper
    {
        private Func<string, string, string, string, string> getBetTypeNameWithResourceId = RefData.GetBetTypeName;
        private Func<string, string, string> getBetTypeName = RefData.GetBetTypeName;
        private Func<string, string, string, string> getSportNameWithDefaultValue = RefData.GetSportName;
        private Func<string, string, string, string, string> getResourceDataByRefIdAndResourceIdWithDefaultValue = RefData.GetResourceDataByRefIdAndResourceId;

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

        public void SetRemotingRefFunctions(
            Func<string, string, string, string, string> getBetTypeNameWithResourceIdFunc = null,
            Func<string, string, string> getBetTypeNameFunc = null,
            Func<string, string, string, string> getSportNameWithDefaultValueFunc = null,
            Func<string, string, string, string, string> getResourceDataByRefIdAndResourceIdWithDefaultValueFunc = null)
        {
            getBetTypeNameWithResourceId = getBetTypeNameWithResourceIdFunc;
            getBetTypeName = getBetTypeNameFunc;
            getSportNameWithDefaultValue = getSportNameWithDefaultValueFunc;
            getResourceDataByRefIdAndResourceIdWithDefaultValue = getResourceDataByRefIdAndResourceIdWithDefaultValueFunc;
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
                getBetTypeNameWithResourceId(betTypeId.ToString(), CurrentLanguageCode, string.Empty, resourceId);
        }

        public string GetBetTypeName(object bettypeId)
        {
            return bettypeId != null
                ? getBetTypeName(bettypeId.ToString(), CurrentLanguageCode)
                : string.Empty;
        }

        public string GetSportName(object sportId)
        {
            return sportId == null ?
                string.Empty :
                getSportNameWithDefaultValue(sportId.ToString(), CurrentLanguageCode, string.Empty);
        }

        public string GetProductName(int productId)
        {
            return getResourceDataByRefIdAndResourceIdWithDefaultValue("Product", productId.ToString(), CurrentLanguageCode, string.Empty);
        }
    }
}

namespace Alpha.ViewLogs.Services.Impl
{
    using System.Threading;
    using Fanex.RemotingRef;

    public class RemotingRefWrapperImpl : RemotingRefWrapper
    {
        private readonly IRemotingRefService remotingRefService;

        public RemotingRefWrapperImpl(IRemotingRefService remotingRefService)
        {
            this.remotingRefService = remotingRefService;
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
                remotingRefService.GetBetTypeName(betTypeId.ToString(), LanguageCode, string.Empty, resourceId);
        }

        public string GetBetTypeName(object bettypeId)
        {
            return bettypeId != null
                ? remotingRefService.GetBetTypeName(bettypeId.ToString(), LanguageCode)
                : string.Empty;
        }

        public string GetSportName(object sportId)
        {
            return sportId == null ?
                string.Empty :
                remotingRefService.GetSportName(sportId.ToString(), LanguageCode, null) ?? remotingRefService.GetResourceDataByRefIdAndResourceId(
                "DummySport",
                sportId.ToString(),
                LanguageCode,
                string.Empty);
        }

        public string GetProductName(int productId)
        {
            return remotingRefService.GetResourceDataByRefIdAndResourceId(
                "Product",
                productId.ToString(),
                LanguageCode,
                string.Empty);
        }

        public string GetSubProductGroupName(int subGroupId)
        {
            return remotingRefService.GetResourceDataByRefIdAndResourceId("SubGroup", subGroupId.ToString(), LanguageCode, string.Empty);
        }

        public string GetProductGroupName(int productGroupId)
        {
            return remotingRefService.GetResourceDataByRefIdAndResourceId("ProductGroup", productGroupId.ToString(), LanguageCode, string.Empty);
        }

        public string GetSportbookSMACommissionName(int sportType, int betType)
        {
            return remotingRefService.GetResourceDataByRefIdAndResourceId("SB_Comm_SMA", $"1_150_{sportType}_{betType}", LanguageCode, string.Empty);
        }

        public string GetBetSettingName(int productGroupId, int groupType, int sportType, int betType)
        {
            return remotingRefService.GetResourceDataByRefIdAndResourceId("G_GT_Sport_Bettype", $"{productGroupId}_{groupType}_{sportType}_{betType}", LanguageCode, string.Empty);
        }

        private static string LanguageCode
            => Thread.CurrentThread.CurrentCulture.Name;
    }
}

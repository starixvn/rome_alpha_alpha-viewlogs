namespace Alpha.ViewLogs.Services
{
    public interface RemotingRefWrapper
    {
        string GetBetType(object betTypeId, object betId = null);

        string GetBetTypeName(object bettypeId);

        string GetSportName(object sportId);

        string GetProductName(int productId);

        string GetSubProductGroupName(int subGroupId);

        string GetProductGroupName(int productGroupId);

        string GetSportbookSMACommissionName(int sportType, int betType);

        string GetBetSettingName(int productGroupId, int groupType, int sportType, int betType);
    }
}

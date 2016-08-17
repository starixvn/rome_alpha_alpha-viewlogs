namespace Alpha.ViewLogs.Constants
{
    using AppResources;
    using System.Collections.Generic;

    public static class PageAccessType
    {
        public const string CreateAccount = "[A]";
        public const string Transfer = "[C]";
        public const string UpdateAccount = "[B]";
        public const string ViewLog = "[G]";
        public const string ViewMemberInfo = "[H]";
        public const string ViewReports = "[E]";
        public const string ViewTotalBets = "[D]";

        public static IDictionary<string, string> PageAccessTypeName()
        {
            var permissions = new Dictionary<string, string>();

            permissions.Add(CreateAccount, Resources.MemberInfo + ": " + Resources.FullControl);
            permissions.Add(Transfer, Resources.Transfer);
            permissions.Add(UpdateAccount, Resources.MemberInfo + ": " + Resources.Update);
            permissions.Add(ViewLog, Resources.ViewLog);
            permissions.Add(ViewMemberInfo, Resources.MemberInfo + ": " + Resources.View);
            permissions.Add(ViewReports, Resources.Reports);
            permissions.Add(ViewTotalBets, Resources.TotalBets + "/" + Resources.Forecast);

            return permissions;
        }
    }
}

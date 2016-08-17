namespace Alpha.ViewLogs.Site.ViewModels.Login
{
    using Models.Enumerations;
    using Params;
    using ViewLogs.Helpers;

    public class IndexViewModel
    {
        private readonly bool hasViewLogPermission;
        private readonly bool isSubAccount;
        private readonly byte roleId;

        public IndexViewModel(BaseGetLogsParam logsParam)
        {
            this.hasViewLogPermission = logsParam.HasViewLogPermission;
            this.isSubAccount = logsParam.SubAccId > 0;
            this.roleId = logsParam.RoleId;
        }

        public bool HasViewLogPermission
        {
            get
            {
                return hasViewLogPermission;
            }
        }

        public bool IsSubAccount
        {
            get
            {
                return isSubAccount;
            }
        }

        public byte RoleId
        {
            get
            {
                return this.roleId;
            }
        }

        public string UserLevel
        {
            get
            {
                return Enumeration.FromValue<UserLevel>(this.roleId).DisplayName.ToLower();
            }
        }

        public GetLogsViewModel GetLogsViewModel { get; set; }

        public string ShowOldValuesCookieName { get; set; }

        public string PreviousUsername { get; set; }

        public string IndexPath
        {
            get
            {
                return MicroSites.ViewLogsPath("~/login/index");
            }
        }

        public string UsernameAutoSuggestionPath
        {
            get
            {
                return MicroSites.OldMainPath("~/_GlobalResources/Handlers/QueryUserName.ashx");
            }
        }

        public string ViewIPInfoPath
        {
            get
            {
                return MicroSites.OldMainPath("~/_IPInfo/IpInfo.aspx");
            }
        }

        public string ActionTypeId { get; set; }
    }
}
namespace Sunplus.Agent.UI._SubAcc
{
	using Resources;
	using Sunplus.Agent.Model.Entities;
	using System.Collections.Generic;

	public class Permissions
    {
		public const string TotalBets = "chkTotalBets";
		public const string Reports = "chkReports";
		public const string CreateAccount = "chkCreateAcc";
		public const string UpdateAccount = "chkUpdateAcc";
		public const string Transfer = "chkTransfer";
		public const string ViewLog = "chkViewLog";
		public const string ViewMemberInfo = "chkMemberInfo";

		public static IDictionary<string, string> GetPermissions()
        {
            // If one more permission is added, only add one item to perm
            var perm = new Dictionary<string, string>()
            {
                { TotalBets, Lang.totalbets + "/" + Lang.lblForecast },
                { Reports, Lang.reports },
                { CreateAccount, Lang.createaccount },
                { UpdateAccount, Lang.updateaccount },
                { ViewMemberInfo, Lang.view },
                { Transfer, Lang.transfer },        
                { ViewLog, Lang.viewlog }
            };

            return perm;
        }

        public static IDictionary<string, bool> GetPageAccess(PageAccess access)
        {
            var pageAccess = new Dictionary<string, bool>()
            {                
                { PageAccessType.ViewTotalBets, access.ViewTotalBets },
                { PageAccessType.ViewReports, access.ViewReports },
                { PageAccessType.CreateAccount, access.CreateAccount },
                { PageAccessType.UpdateAccount, access.UpdateAccount },
                { PageAccessType.ViewMemberInfo, access.ViewMemberInfo },
                { PageAccessType.Transfer, access.Transfer },                
                { PageAccessType.ViewLog, access.ViewLog }                
            };

            return pageAccess;
        }

        public static IDictionary<string, string> GetPermissionsDescription()
        {
            var permissions = new Dictionary<string, string>()
            {
                { PageAccessType.ViewTotalBets, Lang.totalbets + "/" + Lang.lblForecast },
                { PageAccessType.ViewReports, Lang.reports },
                { PageAccessType.CreateAccount, Lang.memberinfo + ": " + Lang.FullControl },
                { PageAccessType.UpdateAccount, Lang.memberinfo + ": " + Lang.update },
                { PageAccessType.ViewMemberInfo, Lang.memberinfo + ": " + Lang.view },
                { PageAccessType.Transfer, Lang.transfer },
                { PageAccessType.ViewLog, Lang.viewlog }
            };

            return permissions;
        }
    }
}
namespace Alpha.ViewLogs._ViewLog
{
	using Sunplus.Agent.Model.BL.Authorization;
	using Sunplus.Agent.Model.Entities;
	using Sunplus.Agent.UI._ViewLog;
	using Sunplus.Shared.ViewLogLib.Entities;
	using Sunplus.Shared.ViewLogLib.Services;
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public partial class LogLogin : ViewLogPageBase
	{
		protected override void PageLoadEvent(object sender, EventArgs e)
		{
			DataControl = rptDataLog;
			NoInfoLabel = ltrNoInfo;
			CurrentUrl = "LogLogin.aspx";
		}

		public override IRequestViewLogContext CreateRequestViewLogContext()
		{
			IRequestViewLogContext requestViewLogContext;
			if (Sunplus.Agent.Model.Entities.UserSession.Current.RoleId == 2) // For Agent level
			{
				var isSubAccount = false;
				var customerId = -1;

				AuthorizationBO bo = new AuthorizationBO(Session.SessionID);

				bool isValidDownline = bo.IsUsernameBelong(CurrentUserSession.UserName,
					UserSession.Current.CustId,
					out customerId, out isSubAccount);

				if (!isValidDownline)
				{
					return null;
				}

				requestViewLogContext = new RequestViewLogContext
				{
					IsInternal = CurrentUserSession.IsInternal,
					CustomerName = CurrentUserSession.UserName,
					FromDate = this.FromDate,
					ToDate = this.ToDate,
					CustomerId = customerId,
					IsSubCust = isSubAccount,
					LanguageCode = ToLanguageCode(CurrentUserSession.LangKey)
				};

				return requestViewLogContext;
			}

			requestViewLogContext = base.CreateRequestViewLogContext();
			// #18125: because Based Page always set IsInternal = FALSE
			// so we MUST reset IsInternal here.
			if (requestViewLogContext != null)
			{
				requestViewLogContext.IsInternal = CurrentUserSession.IsInternal;
			}

			return requestViewLogContext;
		}

		protected override IList GetLogData(out int totalRecords)
		{
			totalRecords = 0;
			var entityList = new List<Sunplus.Shared.ViewLogLib.Entities.LogLogin>();

			var requestViewLogContext = this.RequestViewLogContext;
			if (requestViewLogContext != null)
			{
				entityList = (new LogService<Sunplus.Shared.ViewLogLib.Entities.LogLogin>()).GetPaged(requestViewLogContext, this.PageIndex, this.PageSize, out totalRecords, null);
			}

			return entityList;
		}
	}
}
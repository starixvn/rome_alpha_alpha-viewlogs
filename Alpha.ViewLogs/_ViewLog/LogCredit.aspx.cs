namespace Alpha.ViewLogs._ViewLog
{
	using Sunplus.Agent.UI._ViewLog;
	using Sunplus.Shared.ViewLogLib.Services;
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public partial class LogCredit : ViewLogPageBase
	{
		protected override void PageLoadEvent(object sender, EventArgs e)
		{
			DataControl = rptDataLog;
			NoInfoLabel = ltrNoInfo;
			CurrentUrl = "LogCredit.aspx";
		}

		protected override IList GetLogData(out int totalRecords)
		{
			totalRecords = 0;
			var entityList = new List<Sunplus.Shared.ViewLogLib.Entities.LogCredit>();

			var requestViewLogContext = this.RequestViewLogContext;
			if (requestViewLogContext != null)
			{
				entityList = (new LogService<Sunplus.Shared.ViewLogLib.Entities.LogCredit>()).GetPaged(requestViewLogContext, this.PageIndex, this.PageSize, out totalRecords, null);
			}

			return entityList;
		}
	}
}
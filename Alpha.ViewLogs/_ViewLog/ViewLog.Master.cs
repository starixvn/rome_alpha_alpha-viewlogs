namespace Alpha.ViewLogs._ViewLog
{
	using Fanex.Age.UI.Components;
	using Sunplus.Agent.Model.Entities;
	using Sunplus.Agent.UI._ViewLog;
	using Sunplus.Shared.ViewLogLib;
	using Sunplus.Shared.ViewLogLib.Entities;
	using Sunplus.Shared.ViewLogLib.Services;
	using System;
	using System.Collections.Generic;
	using System.Web.UI.WebControls;

	public partial class ViewLog : System.Web.UI.MasterPage
	{
		public IUserSession CurrentUserSession
		{
			get
			{
				return UserSession.Current;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (!UserSession.Current.SubPageAccess.ViewLog)
					Response.Redirect("../AccessDenied.aspx");

				if (Request.QueryString["tabid"] != null && Request.QueryString["tabid"].ToString().Equals("CustomerSetting", StringComparison.OrdinalIgnoreCase))
				{
					LoadActionType();
				}

				if (CurrentUserSession.RoleId == 2 && Request.QueryString["tabid"].ToLower() == "login")
				{
					divUserName.Style.Add("display", "none");
				}


				DateRangeSelect_Control DateRangeSelect = new DateRangeSelect_Control(null, false, DateTime.Today, DateTime.Today);

				if ((Request["todate"] == null && Request["fromdate"] == null) && DateTime.Today.Day == 1)
				{
					DateRangeSelect.InitializeDropdownList(DateTime.Today, DateTime.Today, NumberOfMonth());
				}
				else
				{
					DateRangeSelect.InitializeDropdownList(Request["fromdate"] == null ? DateTime.Today.AddDays(-1) : PageBase.ConvertDateTime(Request["fromdate"]), Request["todate"] == null ? DateTime.Today : PageBase.ConvertDateTime(Request["todate"]), NumberOfMonth());
				}
				DateRangeSelect.HideTodayYesterdayButton = true;

				ltrDate.Text = DateRangeSelect.ToString();

			}

			// Hot fix for Opera browser (IIS does sniffing user agent)
			Response.ContentType = "text/html";

			base.OnLoad(e);
		}

		protected int NumberOfMonth()
		{
			// Louis: Limit time range for view log in 2 years (start from beginning of last year)
			DateTime startdate = new DateTime(DateTime.Today.AddYears(-1).Year, 1, 1);
			DateTime enddate = DateTime.Today;

			int M = Math.Abs((startdate.Year - enddate.Year));
			int numofmonth = ((M * 12) + Math.Abs((startdate.Month - enddate.Month)));
			return numofmonth;
		}

		protected void LoadActionType()
		{
			List<LogActionType> list = (new LogActionTypeService()).GetAll();

			if (!ReferenceEquals(list, null))
			{
				ddlActionType.DataValueField = "ActionTypeID";
				ddlActionType.DataTextField = "ActionTypeName" + PageBase.ToLanguageCode(Sunplus.Agent.Model.Entities.UserSession.Current.LangKey).ToString();
				ddlActionType.DataSource = list;
				ddlActionType.DataBind();

				//add first dropdownlist item (all)
				var listItem = new ListItem { Text = Resources.Viewlog.All, Value = "-1" };
				ddlActionType.Items.Insert(0, listItem);
			}
		}

		/// <summary>
		/// sub account doest not have update permission can
		///  view loglogin.aspx only
		/// </summary>
		public bool VisibleMenuItem
		{
			get
			{
				ViewLogPageBase viewLogPageBase = (this.Page as ViewLogPageBase);
				if (viewLogPageBase != null)
				{
					return !viewLogPageBase.IsSubAaccountAndCannotViewLog;
				}
				return false;
			}
		}
	}
}
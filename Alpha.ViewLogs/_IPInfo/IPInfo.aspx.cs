namespace Sunplus.Agent.UI._IPInfo
{
	using Sunplus.Core.Common.IPs;
	using Sunplus.Core.Common.IPs.DAL;
	using System;
	using System.Web.UI;

	public partial class IPInfo_Page : Fanex.Age.UI.TplPage
	{
		public string IP
		{
			get
			{
				string ip = Request.QueryString["ip"];
				if (String.IsNullOrEmpty(ip)) ip = Sunplus.Agent.Model.Common.AgentIPFormat.GetClientIP();
				return ip;
			}
		}

		public override object InvokeData(System.Collections.Hashtable p)
		{
			return null;
		}

		public override void RenderContent(HtmlTextWriter writer)
		{
			IpInfo_Template template = new IpInfo_Template();
			template.lblip = Alpha.ViewLogs.LocalResources.Lang.ip;
			template.lblcountry = Alpha.ViewLogs.LocalResources.Lang.country;
			template.lblcity = Alpha.ViewLogs.LocalResources.Lang.city;
			template.ipnote = string.Format(Alpha.ViewLogs.LocalResources.Lang.ipnote, "Whois.ashx?ip=" + this.IP);

			string currentIP = this.IP;
			IPTable ipInfo = IpUtils.LocationFromIP(currentIP);
			template.ip = currentIP;
			template.countryname = ipInfo.CountryLong;
			template.cityname = ipInfo.City;
			template.flagpath = "../Flags/" + ipInfo.CountryShort + ".gif";
			writer.Write(template);
		}

		public override void ProcessRequest(ref System.Collections.Hashtable p)
		{
		}

		public override object ValidateRequest(System.Collections.Hashtable p)
		{
			return null;
		}
	}
}
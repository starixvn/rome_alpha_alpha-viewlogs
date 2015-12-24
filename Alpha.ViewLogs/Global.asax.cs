namespace Alpha.ViewLogs
{
	using System;
	using System.Reflection;
	using System.Web;

	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();

			CheatApplicationName();
		}

		private void CheatApplicationName()
		{
			string applicationName = "AlphaSite";

			// Change the Application Name in runtime.
			var runtimeInfo = typeof(HttpRuntime).GetField("_theRuntime", BindingFlags.Static | BindingFlags.NonPublic);
			var theRuntime = (HttpRuntime)runtimeInfo.GetValue(null);
			var appNameInfo = typeof(HttpRuntime).GetField("_appDomainAppId", BindingFlags.Instance | BindingFlags.NonPublic);

			appNameInfo.SetValue(theRuntime, applicationName);
		}
	}
}
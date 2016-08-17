namespace Alpha.ViewLogs.Site.Controllers
{
    using Constants;
    using System.Web.Mvc;
    using ViewModels;

    public class TabController : Controller
    {
        [ChildActionOnly]
        public ActionResult Index(string tabName = TabType.CustomerSetting)
        {
            return PartialView(new TabViewModel(tabName));
        }
    }
}
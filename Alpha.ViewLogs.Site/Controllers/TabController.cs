namespace Alpha.ViewLogs.Site.Controllers
{
    using System.Web.Mvc;
    using Constants;
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
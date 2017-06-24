using LeaRun.Application.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.PublicInfoManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2016.10.18
    /// 描 述：电子签章
    /// </summary>
    public class SignetController : MvcControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}


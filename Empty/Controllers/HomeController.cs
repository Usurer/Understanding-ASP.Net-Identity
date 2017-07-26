using System.Web;
using System.Web.Mvc;

namespace Empty.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var identity = HttpContext.GetOwinContext().Authentication.User.Identity;
            if (identity.IsAuthenticated)
            {
                return View((object)identity.Name);
            }
            return View((object)"Not Authenticated");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Buy.UI.Web.Controllers
{
    public class LandingController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}

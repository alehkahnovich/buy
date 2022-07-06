using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Buy.UI.Web.Controllers
{
    public class SessionController : Controller {
        [Authorize]
        public IActionResult Index() {
            return RedirectToAction("index", "landing");
        }
    }
}
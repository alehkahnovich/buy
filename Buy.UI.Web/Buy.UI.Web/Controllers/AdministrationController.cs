using Microsoft.AspNetCore.Mvc;

namespace Buy.UI.Web.Controllers
{
    public class AdministrationController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
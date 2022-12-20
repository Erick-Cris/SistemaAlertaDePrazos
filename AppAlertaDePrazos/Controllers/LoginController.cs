using Microsoft.AspNetCore.Mvc;

namespace AppAlertaDePrazos.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

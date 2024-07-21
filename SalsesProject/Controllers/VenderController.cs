using Microsoft.AspNetCore.Mvc;

namespace SalsesProject.Controllers
{
    public class VenderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

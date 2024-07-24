using Microsoft.AspNetCore.Mvc;

namespace SalsesProject.Controllers
{
    public class CurrentItemInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SalsesProject.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

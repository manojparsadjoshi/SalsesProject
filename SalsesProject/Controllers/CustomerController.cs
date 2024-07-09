using Microsoft.AspNetCore.Mvc;

namespace SalsesProject.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SalsesProject.Controllers
{
    public class SalesReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

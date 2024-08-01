using Microsoft.AspNetCore.Mvc;

namespace SalsesProject.Controllers
{
    public class PurchaseReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

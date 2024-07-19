using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Services.PurchaseMasterDetail;
using Sales.Services.PurchaseMasterDetail.ViewModel;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseMasterDetailAPIController : ControllerBase
    {
        private IPurchaseMasterSercices _purchaseMasterSercices;
        public PurchaseMasterDetailAPIController(IPurchaseMasterSercices purchaseMasterSercices)
        {
            _purchaseMasterSercices = purchaseMasterSercices;
        }

        [HttpPost]
        public bool Add(PurchaseMasterVM model)
        {
            return _purchaseMasterSercices.Add(model);
        }
    }
}

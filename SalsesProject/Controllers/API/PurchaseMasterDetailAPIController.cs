using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Services.PurchaseMasterDetail;
using Sales.Services.PurchaseMasterDetail.ViewModel;
using Sales.Services.Vender.ViewModel;
using SalsesProject.Models.VM;

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
        [HttpGet]
        public List<PurchaseMasterVM>GetAll()
        {
            return _purchaseMasterSercices.GetAll();
        }
        [HttpGet("Id")]
        public PurchaseMasterVM GetById(int id)
        {
            return _purchaseMasterSercices.GetById(id);
        }
        [HttpPost]
        public bool Add(PurchaseMasterVM model)
        {
            return _purchaseMasterSercices.Add(model);
        }
        [HttpDelete]
        public int Delete(int id)
        {
            return _purchaseMasterSercices.Delete(id);
        }

        [HttpPut]
        public bool Update(PurchaseMasterVM model)
        {
            return _purchaseMasterSercices.Update(model);
        }

        [HttpGet("ItemName")]
        //[product]
        public IEnumerable<GetItemsNameVM> GetItemsNames()
        {
            return _purchaseMasterSercices.GetItemsNames();
        }
        [HttpGet("VenderName")]
        public IEnumerable<GetVendersName> GetVendersNames()
        {
           return _purchaseMasterSercices.GetVendersNames();
        }

        [HttpGet("Report")]
        public List<PurchaseReportVM> GetPurchaseReports()
        {
            return _purchaseMasterSercices.GetPurchaseReports();
        }
    }
}

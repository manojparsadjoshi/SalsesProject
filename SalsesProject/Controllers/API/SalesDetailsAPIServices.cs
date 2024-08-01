using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Services.MasterDetail;
using Sales.Services.MasterDetail.ViewModel;
using SalsesProject.Models.VM;
using static Sales.Services.MasterDetail.SalesDetailsServices;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SalesDetailsAPIServices : ControllerBase
    {
        private readonly ISalesDetailsServices _salesDetailsServices;
        public SalesDetailsAPIServices(ISalesDetailsServices salesDetailsServices)
        {
            _salesDetailsServices = salesDetailsServices;
        }
        [HttpGet]
        public List<SalesMasterVM> GetAll()
        {
            return _salesDetailsServices.GetAll();
        }
        [HttpGet("{id}")]
        public SalesMasterVM GetById(int id)
        {
            return _salesDetailsServices.GetById(id);
        }
        [HttpPost]
        public ResponseModel Add(SalesMasterVM vm)
        {
            return _salesDetailsServices.Create(vm);
        }
        [HttpPut]
        public ResponseModel Update(SalesMasterVM obj)
        {
            return _salesDetailsServices.Update(obj);
        }
        [HttpDelete]
        public int Delete(int id)
        {
            return _salesDetailsServices.Delete(id);
        }
        [HttpGet]
        public IEnumerable<GetCustomersNameVM> GetCustomersName()
        {
            return _salesDetailsServices.GetCustomersName();
        }

        [HttpGet]
        public IEnumerable<GetItemsNameVM> GetItemsName()
        {
            return _salesDetailsServices.GetItemsName();
        }
        [HttpGet]
        public List<SalesReportVM> GetSalesReports()
        {
            return _salesDetailsServices.GetSalesReports();
        }

    }
}

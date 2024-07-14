using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Services.MasterDetail;
using SalsesProject.Models.VM;

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
        public bool Add(SalesMasterVM vm)
        {
            return _salesDetailsServices.Create(vm);
        }
        [HttpPut]
        public bool Update(SalesMasterVM obj)
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

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalsesProject.Models.VM;
using SalsesProject.Services;

namespace SalsesProject.Controllers
{
    [Route("api/[controller]")]
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
    }
}

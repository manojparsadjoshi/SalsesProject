using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Db.Migrations;
using Sales.Entity;
using Sales.Services.Vender;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenderAPIController : ControllerBase
    {
        private readonly IVenderServices _venderServices;
        public  VenderAPIController(IVenderServices venderServices)
        {
            _venderServices = venderServices;
        }
        [HttpGet("GetAll")]
        public List<VenderModel> Get()
        {
            var datalist =  _venderServices.GetAll();
            return datalist;          
        }
        [HttpPost]
        public bool Create(VenderModel vender)
        {
            if(vender == null)
            {
                return false;
            }
            var data = _venderServices.Create(vender);
            return true;
        }
    }
}

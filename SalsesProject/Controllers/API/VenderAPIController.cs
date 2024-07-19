using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("id")]
        public ActionResult<VenderModel> GetById(int id)
        {
            var existingData = _venderServices.GetById(id);
            if (existingData == null)
            {
                return NotFound();
            }
            return Ok(existingData);
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var existingData = _venderServices.GetById(id);
            if(existingData == null)
            {
                return NotFound();
            }
            _venderServices.Delete(id);
            return Ok(id);
        }
    }
}

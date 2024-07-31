using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Services.Item;
using SalsesProject.Models;
using SalsesProject.Models.VM;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {

        private readonly IItemServices _services;
        public ItemAPIController(IItemServices services)
        {

            _services = services;
        }
        [HttpGet]
        public IActionResult GetItems()
        {
            List<ItemsModel> items = _services.GetAll();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }
        [HttpPost]
        public ItemResult Create(ItemsModel item)
        {
            return _services.Create(item);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _services.GetById(id);
            if (id == 0)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPut("{id}")]
        public ItemResult Update(ItemsModel item)
        {
           return _services.Update(item);
        }
        [HttpDelete]
        public int Delete(int id)
        {
            _services.Delete(id);
            return id;
        }
        [HttpGet("GetCategory")]
        public List<DropdownVM> GetCategory()
        {
            List<DropdownVM> result = Enum.GetValues(typeof(Category))
                                     .Cast<Category>()
                                     .Select(c => new DropdownVM
                                     {
                                         Id = (int)c,
                                         Name = c.ToString(),
                                         Unit = c.ToString()
                                     }).ToList();

            return result;
        }
    }
}

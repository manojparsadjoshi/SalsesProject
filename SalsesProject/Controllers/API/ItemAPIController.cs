using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalsesProject.Data;
using SalsesProject.Models;
using SalsesProject.Services;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IItemServices _services;
        public ItemAPIController(ApplicationDbContext context, IItemServices services)
        {
            _context = context;
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
        public bool Create(ItemsModel item)
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
        public void Update(ItemsModel item)
        {
            _services.Update(item);
        }
        [HttpDelete]
        public int Delete(int id)
        {
            _services.Delete(id);
            return id;
        }
    }
}

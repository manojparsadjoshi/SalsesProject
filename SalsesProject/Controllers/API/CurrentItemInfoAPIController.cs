



using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Db;
using Sales.Entity;
using SalsesProject.Models;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentItemInfoAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CurrentItemInfoAPIController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<ItemCurrentInfo> GetItems()
        {
            var existingitemsdata = _context.Items.ToList();
            var existingcurrentitems = _context.itemCurrentInfos.ToList();

            var requireditems = (from items in existingitemsdata
                                 join item in existingcurrentitems on items.ItemId equals item.ItemId
                                 select new ItemCurrentInfo
                                 {
                                     Id = item.Id,
                                     ItemId = item.ItemId, 
                                     ItemName = items.ItemName,
                                     Quentity = item.Quentity
                                 }).ToList();
            return requireditems;
           
        }
       
       
    }
}


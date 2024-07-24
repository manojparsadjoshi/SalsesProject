using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Db;
using Sales.Entity;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemHistoryAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemHistoryAPIController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<ItemCurrentInfoHistoryModel> GetItems()
        {
            var historyData = _context.InfoHistoryModels.ToList();
            var itemData = _context.Items.ToList();
            var result = (from infoHistory in  historyData
                          join item in itemData on infoHistory.ItemId equals item.ItemId
                          select new ItemCurrentInfoHistoryModel
                          {
                              Id = infoHistory.Id,  
                              ItemId = infoHistory.ItemId,
                              Item = item,
                              Quentity = infoHistory.Quentity,
                              TransDate = infoHistory.TransDate,
                              StockInOut = infoHistory.StockInOut,
                              TransactionType = infoHistory.TransactionType,
                          }).ToList();
            return result;
        }

        
    }
}

using Sales.Db;
using SalsesProject.Models;

namespace Sales.Services.Item
{
    public class ItemResult
    {
        public bool Success { get; set; }
        public ItemsModel Data { get; set; }
    }
    public class ItemServices : IItemServices
    {
        private readonly ApplicationDbContext _context;
        public ItemServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public ItemResult Create(ItemsModel items)
        {
            var existingItem = _context.Items.FirstOrDefault(x => x.ItemName.ToLower() == items.ItemName.ToLower());
            if (existingItem != null)
            {
                return new ItemResult { Success = false };
            }
            _context.Items.Add(items);
            _context.SaveChanges();
            return new ItemResult { Success = true, Data = items };
        }

        public int Delete(int id)
        {
            var data = _context.Items.Find(id);
            if (data != null)
            {
                _context.Items.Remove(data);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public List<ItemsModel> GetAll()
        {
            var items = _context.Items.ToList();
            return items;
        }

        public ItemsModel GetById(int id)
        {
            var data = _context.Items.Find(id);
            return data;
        }

        public ItemResult Update(ItemsModel item)
        {
            var itemdata = _context.Items.Find(item.ItemId);
            bool existingItem = _context.Items.Any(x => x.ItemName.ToLower() == item.ItemName.ToLower() && x.ItemId != item.ItemId);
            if (existingItem)
            {
                return new ItemResult { Success = false };
            }
            itemdata.ItemName = item.ItemName;
            itemdata.Unit = item.Unit;
            itemdata.Category = item.Category;
            _context.Items.Update(itemdata);
            _context.SaveChanges();
            return new ItemResult { Success = true, Data = item };

        }
    }   
}

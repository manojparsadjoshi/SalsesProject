using Sales.Db;
using SalsesProject.Models;

namespace Sales.Services.Item
{
    public class ItemServices : IItemServices
    {
        private readonly ApplicationDbContext _context;
        public ItemServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(ItemsModel items)
        {
            if (items == null)
            {
                return false;
            }
            _context.Items.Add(items);
            _context.SaveChanges();
            return true;

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

        public bool Update(ItemsModel item)
        {
            var itemdata = _context.Items.Find(item.ItemId);
            if (itemdata != null)
            {
                itemdata.ItemName = item.ItemName;
                itemdata.Unit = item.Unit;
                itemdata.Category = item.Category;
                _context.Items.Update(itemdata);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

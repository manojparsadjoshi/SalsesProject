using SalsesProject.Models;

namespace Sales.Services.Item
{
    public interface IItemServices
    {
        List<ItemsModel> GetAll();
        ItemsModel GetById(int id);
        bool Create(ItemsModel item);
        bool Update(ItemsModel item);
        int Delete(int id);
    }
}

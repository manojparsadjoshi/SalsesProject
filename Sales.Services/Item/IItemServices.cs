using SalsesProject.Models;

namespace Sales.Services.Item
{
    public interface IItemServices
    {
        List<ItemsModel> GetAll();
        ItemsModel GetById(int id);
        ItemResult Create(ItemsModel item);
        ItemResult Update(ItemsModel item);
        int Delete(int id);
    }
}

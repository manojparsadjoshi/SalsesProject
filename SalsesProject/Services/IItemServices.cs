using SalsesProject.Models;

namespace SalsesProject.Services
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

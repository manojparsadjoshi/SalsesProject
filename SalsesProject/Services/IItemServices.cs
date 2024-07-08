using SalsesProject.Models;

namespace SalsesProject.Services
{
    public interface IItemServices
    {
        List<ItemsModel> GetAll();
        ItemsModel GetById(int id);
        int Create(ItemsModel item);
        int Update(ItemsModel item);
        int Delete(int id);
    }
}

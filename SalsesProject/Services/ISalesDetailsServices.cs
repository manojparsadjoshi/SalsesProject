using SalsesProject.Models.VM;

namespace SalsesProject.Services
{
    public interface ISalesDetailsServices
    {
        List<SalesMasterVM> GetAll();
        SalesMasterVM GetById(int Id);
        bool Create(SalesMasterVM vm);
        bool Update(SalesMasterVM obj);
        int Delete(int id);
        IEnumerable<GetCustomersNameVM> GetCustomersName();
        IEnumerable<GetItemsNameVM> GetItemsName();
    }
}

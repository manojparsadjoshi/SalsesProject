using SalsesProject.Models.VM;

namespace Sales.Services.MasterDetail
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

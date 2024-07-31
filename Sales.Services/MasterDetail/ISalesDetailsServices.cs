using SalsesProject.Models.VM;
using static Sales.Services.MasterDetail.SalesDetailsServices;

namespace Sales.Services.MasterDetail
{
    public interface ISalesDetailsServices
    {
        List<SalesMasterVM> GetAll();
        SalesMasterVM GetById(int Id);
        ResponseModel Create(SalesMasterVM vm);
        ResponseModel Update(SalesMasterVM obj);
        int Delete(int id);
        IEnumerable<GetCustomersNameVM> GetCustomersName();
        IEnumerable<GetItemsNameVM> GetItemsName();
    }
}

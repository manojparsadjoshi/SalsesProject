using Sales.Services.PurchaseMasterDetail.ViewModel;
using Sales.Services.Vender.ViewModel;
using SalsesProject.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services.PurchaseMasterDetail
{
    public interface IPurchaseMasterSercices
    {
        List<PurchaseMasterVM> GetAll();
        PurchaseMasterVM GetById(int id);
        bool Add(PurchaseMasterVM model);
        bool Update(PurchaseMasterVM model);
        int Delete(int id);
        public IEnumerable<GetVendersName> GetVendersNames();
        public IEnumerable<GetItemsNameVM> GetItemsNames();

    }
}

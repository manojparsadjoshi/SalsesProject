using Sales.Entity;
using SalsesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services.Vender
{
    public interface IVenderServices
    {
        List<VenderModel> GetAll();
        VenderModel GetById(int id);
        bool Create(VenderModel vender);
        bool Update(VenderModel vender);
        bool Delete(int id);
    }
}

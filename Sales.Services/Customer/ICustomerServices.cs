using SalsesProject.Models;

namespace Sales.Services.Customer
{
    public interface ICustomerServices
    {
        List<CustomerModel> GetAll();
        CustomerModel GetById(int Id);
        bool Create(CustomerModel customer);
        bool Update(CustomerModel customer);
        int Delete(int id);
    }
}

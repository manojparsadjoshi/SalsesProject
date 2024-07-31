using SalsesProject.Models;

namespace Sales.Services.Customer
{
    public interface ICustomerServices
    {
        List<CustomerModel> GetAll();
        CustomerModel GetById(int Id);
        CustomerResult Create(CustomerModel customer);
        CustomerResult Update(CustomerModel customer);
        int Delete(int id);
    }
}

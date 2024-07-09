using SalsesProject.Models;

namespace SalsesProject.Services
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

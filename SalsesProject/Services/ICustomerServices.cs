using SalsesProject.Models;

namespace SalsesProject.Services
{
    public interface ICustomerServices
    {
        List<CustomerModel> GetAll();
        CustomerModel GetById(int Id);
        int Create(CustomerModel customer);
        bool Update(CustomerModel customer);
        int Delete(int id);
    }
}

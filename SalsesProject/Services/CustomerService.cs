using Microsoft.AspNetCore.Http.HttpResults;
using SalsesProject.Data;
using SalsesProject.Migrations;
using SalsesProject.Models;

namespace SalsesProject.Services
{
    public class CustomerService : ICustomerServices
    {
        private readonly ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(CustomerModel customer)
        {
            if(customer != null)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return true;
            }
          return false;
        }

        public int Delete(int id)
        {
            var existiingData = _context.Customers.Find(id);
            if(existiingData != null)
            {
                _context.Customers.Remove(existiingData);
                _context.SaveChanges();
                return existiingData.CustomerId;
            }
            return 0;
        }

        public List<CustomerModel> GetAll()
        {                    
               return _context.Customers.ToList();          
        }

        public CustomerModel GetById(int Id)
        {                    
               return _context.Customers.Find(Id);                                
        }

        public bool Update(CustomerModel customer)
        {
            var existingdata = _context.Customers.Find(customer.CustomerId);
            if(existingdata != null)
            {
                existingdata.Address = customer.Address;
                existingdata.CustomerName = customer.CustomerName;
                existingdata.ContactNumber = customer.ContactNumber;
                _context.Customers.Update(existingdata);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

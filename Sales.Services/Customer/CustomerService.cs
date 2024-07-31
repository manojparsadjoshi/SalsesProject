using Sales.Db;
using SalsesProject.Models;

namespace Sales.Services.Customer
{
    public class CustomerResult
    {
        public bool Success { get; set; }
        public CustomerModel Data { get; set; }
    }
    public class CustomerService : ICustomerServices
    {
        private readonly ApplicationDbContext _context;
        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public CustomerResult Create(CustomerModel customer)
        {
            var existingcustomer = _context.Customers.FirstOrDefault(x=>x.CustomerName.ToLower() == customer.CustomerName.ToLower());
            if (existingcustomer != null)
            {
                return new CustomerResult { Success = false };
            }
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return new CustomerResult { Success = true, Data = customer};
        }

        public int Delete(int id)
        {
            var existiingData = _context.Customers.Find(id);
            if (existiingData != null)
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

        public CustomerResult Update(CustomerModel customer)
        {
            var existingdata = _context.Customers.Find(customer.CustomerId);
            bool existingcustomer = _context.Customers.Any(x => x.CustomerName.ToLower() == customer.CustomerName.ToLower() && x.CustomerId != customer.CustomerId);
            {
                return new CustomerResult { Success = false };
            }
             existingdata.Address = customer.Address;
                existingdata.CustomerName = customer.CustomerName;
                existingdata.ContactNumber = customer.ContactNumber;
                _context.Customers.Update(existingdata);
                _context.SaveChanges();
                return new CustomerResult { Success = true, Data = customer};
        }
    }
}

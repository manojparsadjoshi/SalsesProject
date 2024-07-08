using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalsesProject.Data;
using SalsesProject.Models;
using SalsesProject.Services;

namespace SalsesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerServices _services;
        public CustomerAPIController(ApplicationDbContext context, ICustomerServices services)
        {
            _context = context;
            _services = services;
        }
        [HttpGet]
        public IActionResult GetCustomer()
        {
            List<CustomerModel> customers = _services.GetAll();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        [HttpPost("Add")]
        public int Create(CustomerModel customer)
        {
            return (_services.Create(customer));
        }
        [HttpGet("id")]
        public CustomerModel GetCustomerById(int id)
        {
            var customer = _services.GetById(id);
            return customer;
        }
        [HttpPut("id")]
        public void Update(CustomerModel customer)
        {
          _services.Update(customer);
        }
        [HttpDelete("id")]
        public int Delete(int id)
        {
            _services.Delete(id);
            return id;
        }
    }
}
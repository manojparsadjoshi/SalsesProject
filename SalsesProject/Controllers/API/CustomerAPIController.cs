﻿using Microsoft.AspNetCore.Mvc;
using Sales.Services.Customer;
using SalsesProject.Models;

namespace SalsesProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
       
        private readonly ICustomerServices _services;
        public CustomerAPIController( ICustomerServices services)
        {
            
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
        public bool Create(CustomerModel customer)
        {
            return _services.Create(customer);
        }
        [HttpGet("id")]
        public CustomerModel GetCustomerById(int id)
        {
            var customer = _services.GetById(id);
            return customer;
        }
        [HttpPut("id")]
        public bool Update(CustomerModel customer)
        {
            _services.Update(customer); 
            return true;
          
        }
        [HttpDelete("id")]
        public int Delete(int id)
        {
            _services.Delete(id);
            return id;
        }
    }
}
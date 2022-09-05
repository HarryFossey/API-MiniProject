using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindApi.Models;
using NorthwindApi.Models.DTO;
using NorthwindApi.Services;

namespace NorthwindApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _service.GetCustomerListAsync();

            if (customers == null) return NotFound();

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);

            if (customer == null) return NotFound();

            return Ok(customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, Customer customer)
        {
            if (id != customer.CustomerId) return BadRequest();

            var customerById = await _service.GetCustomerByIdAsync(id);

            if (customerById is null) return NotFound();

            customerById.CompanyName = customer.CompanyName ?? customerById.CompanyName;
            customerById.ContactName = customer.ContactName ?? customerById.ContactName;
            customerById.ContactTitle = customer.ContactTitle ?? customerById.ContactTitle;
            customerById.Address = customer.Address ?? customerById.Address;
            customerById.City = customer.City ?? customerById.City;
            customerById.Region = customer.Region ?? customerById.Region;
            customerById.PostalCode = customer.PostalCode ?? customerById.PostalCode;
            customerById.Country = customer.Country ?? customerById.Country;
            customerById.Phone = customer.Phone ?? customerById.Phone;
            customerById.Fax = customer.Fax ?? customerById.Fax;

            await _service.SaveCustomerChangesAsync();

            return NoContent();
        }

        /*
        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            
            if (_context.Customers == null)
            {
                return Problem("Entity set 'NorthwindContext.Customers'  is null.");
            }
            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
            
        }
        */

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();

            var customerOrders = await _service.GetOrdersByCustomerIdAsync(id);
            foreach (var order in customerOrders) order.Customer = null;

            await _service.RemoveCustomerAsync(customer);
            return NoContent();
        }
        
        private async Task<bool> CustomerExistsAsync(string id)
        {
            return await _service.GetCustomerByIdAsync(id) is null ? false : true;
        }
    }
}

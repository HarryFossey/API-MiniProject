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

            var customerById = _service.GetCustomerByIdAsync(id);

            /*
            //Null-coalescing oeprator returns the value of it's left hand 
            //operand if it isn't null.
            supplier.CompanyName = supplierDto.CompanyName ?? supplier.CompanyName;
            supplier.ContactName = supplierDto.ContactName ?? supplier.ContactName;
            supplier.ContactTitle = supplierDto.ContactTitle ?? supplier.ContactTitle;
            supplier.Country = supplierDto.Country ?? supplier.Country;
            

            try
            {
                await _service.SaveCustomerChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
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

using Microsoft.EntityFrameworkCore;
using NorthwindApi.Models;

namespace NorthwindApi.Services;

public class CustomerService : ICustomerService
{
    private readonly NorthwindContext _context;

    public CustomerService()
    {
        _context = new NorthwindContext();
    }

    public CustomerService(NorthwindContext context)
    {
        _context = context;
    }

    public async Task CreateCustomerAsync(Customer customer)
    {
        await _context.Customers.AddAsync(customer)!;
        await _context.SaveChangesAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(string customerId)
    {
        return await _context.Customers.Include(x => x.Orders).Where(y => y.CustomerId == customerId).SingleOrDefaultAsync();
    }

    public async Task<List<Customer>> GetCustomerListAsync()
    {
        return await _context.Customers.Include(x => x.Orders).ToListAsync();
    }

    public async Task RemoveCustomerAsync(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrdersByCustomerIdAsync(string id)
    {
        return await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();
    }

    public async Task SaveCustomerChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task AddOrdersAsync(IEnumerable<Order> orders)
    {
        await _context.Orders.AddRangeAsync();
        await _context.SaveChangesAsync();
    }
}

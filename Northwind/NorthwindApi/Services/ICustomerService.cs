using NorthwindApi.Models;

namespace NorthwindApi.Services;

public interface ICustomerService
{
    public Task<List<Customer>> GetCustomerListAsync();
    public Task<Customer> GetCustomerByIdAsync(string customerId);
    public Task CreateCustomerAsync(Customer customer);
    public Task RemoveCustomerAsync(Customer customer);
    public Task<List<Order>> GetOrdersByCustomerIdAsync(string id);
    public Task SaveCustomerChangesAsync();
}

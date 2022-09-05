using NorthwindApi.Models;

namespace NorthwindApi.Services;

public interface ICustomerService
{
    public List<Customer> GetCustomerList();
    public Customer GetCustomerById(string customerId);
    public void CreateCustomer(Customer customer);
    public void RemoveCustomer(Customer customer);
    public void SaveCustomerChanges();
}

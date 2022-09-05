using Microsoft.EntityFrameworkCore;
using NorthwindApi.Models;
using NorthwindApi.Services;
namespace NorthwindTests;

public partial class CustomerServiceShould
{
    private NorthwindContext _context;
    private CustomerService _customerService;

    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var options = new DbContextOptionsBuilder<NorthwindContext>().UseInMemoryDatabase(databaseName: "Memory_DB").Options;
        _context = new NorthwindContext(options);
        _customerService = new CustomerService(_context);
        _customerService.CreateCustomerAsync(new Customer { CustomerId = "MANDA", ContactName = "Nish Mandal", CompanyName = "Sparta Global", City = "Brum" }).Wait();
    }
    

    [Category("Happy Path")]
    [Test]
    public async Task DeleteCustomerWhenGivenValidId()
    {
        // Arrange
        var numberOfCustomersBefore = _context.Customers.Count();
        var customer = await _customerService.GetCustomerByIdAsync("MANDA");

        // Act
        await _customerService.RemoveCustomerAsync(customer);
        var numberOfCustomersAfter = _context.Customers.Count();

        // Assert
        Assert.That(numberOfCustomersBefore, Is.EqualTo(numberOfCustomersAfter + 1));
    }

    [Category("Sad Path")]
    [Test]
    public async Task NotDeleteCustomerWhenGivenInvalidId()
    {
        var customerToRemove = new Customer { CustomerId = "TEST1", ContactName = "Test Test", CompanyName = "Test Test", City = "Test" };

        Assert.That(async () => await _customerService.RemoveCustomerAsync(customerToRemove), Throws.TypeOf<DbUpdateConcurrencyException>());
    }   
}

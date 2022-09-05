using Moq;
using NorthwindApi.Models;
using NorthwindApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace NorthwindTests;

public class CustomerServiceShould
{
    private NorthwindContext _context;
    private CustomerService _sut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var options = new DbContextOptionsBuilder<NorthwindContext>().UseInMemoryDatabase(databaseName: "Example_DB").Options;
        _context = new NorthwindContext(options);
        _sut = new CustomerService(_context);

        //Seed the Database
        _sut.CreateCustomerAsync(new Customer { CustomerId = "BELLA", ContactName = "Peter Bellaby", CompanyName = "Sparta Global", City = "Paris" });
        _sut.CreateCustomerAsync(new Customer { CustomerId = "MANDA", ContactName = "Nish Mandal", CompanyName = "Sparta Global", City = "Brum" });
    }


    [Test]
    public void ReturnACustomer_When_GetCustomerByIdAsyncIsCalledWithAValidId()
    {
        //_sut.GetCustomerByIdAsync(customer)
        var result = _sut.GetCustomerByIdAsync("MANDA");

        Assert.That(result, Is.InstanceOf<Task<Customer>>());
    }

    [Test]
    public void ReturnAListofCustomers_When_GetCustomerListAsyncIsCalledWithAValidId()
    {
        //_sut.GetCustomerByIdAsync(customer)
        var result = _sut.GetCustomerListAsync();

        Assert.That(result, Is.InstanceOf<Task<List<Customer>>>());
    }

    [Category("Happy Path")]
    [Test]
    public async Task DeleteCustomerWhenGivenValidId()
    {
        // Arrange
        var numberOfCustomersBefore = _context.Customers.Count();
        var customer = await _sut.GetCustomerByIdAsync("MANDA");

        // Act
        await _sut.RemoveCustomerAsync(customer);
        var numberOfCustomersAfter = _context.Customers.Count();

        // Assert
        Assert.That(numberOfCustomersBefore, Is.EqualTo(numberOfCustomersAfter + 1));
    }

    /*
    [Category("Sad Path")]
    [Test]
    public async Task NotDeleteCustomerWhenGivenInvalidId()
    {
        var customerToRemove = new Customer { CustomerId = "TEST1", ContactName = "Test Test", CompanyName = "Test Test", City = "Test" };

        Assert.That(async () => await _sut.RemoveCustomerAsync(customerToRemove), Throws.TypeOf<DbUpdateConcurrencyException>());
    }
    */

    [Category("Happy Path")]
    [Test]
    public void GivenANewCustomer_CreateCustomerAddsItToTheDatabase()
    {
        var customer = new Customer() { CustomerId = "DANIE", ContactName = "Daniel Williams", CompanyName = "Sparta Global", City = "Liverpool" };
        _sut.CreateCustomerAsync(customer);
        var result = _sut.GetCustomerByIdAsync("DANIE").Result;
        Assert.That(result, Is.TypeOf<Customer>());
        Assert.That(result.ContactName, Is.EqualTo("Daniel Williams"));
        Assert.That(result.CompanyName, Is.EqualTo("Sparta Global"));
        Assert.That(result.City, Is.EqualTo("Liverpool"));
        _context.Customers.Remove(customer);
        _context.SaveChanges();
    }

    [Category("Sad Path")]
    [Test]
    public void GivenANewCustomer_WithTheSameIdAsExistingCustomer_DoNotOverwrite()
    {
        var customer = new Customer() { CustomerId = "BELLA", ContactName = "Daniel Williams", CompanyName = "Sparta Global", City = "Liverpool" };
        Assert.That(() => _sut.CreateCustomerAsync(customer), Throws.TypeOf<InvalidOperationException>());
        var result = _sut.GetCustomerByIdAsync("BELLA").Result;
        Assert.That(result, Is.TypeOf<Customer>());
        Assert.That(result.ContactName, Is.EqualTo("Peter Bellaby"));
        Assert.That(result.CompanyName, Is.EqualTo("Sparta Global"));
        Assert.That(result.City, Is.EqualTo("Paris"));
        _context.Customers.Remove(customer);
        _context.SaveChanges();
    }

}

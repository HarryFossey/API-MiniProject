using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthwindApi.Controllers;
using NorthwindApi.Models;
using NorthwindApi.Services;
using NUnit.Framework;

namespace NorthwindTests;

public partial class CustomerServiceShould
{
    //private CustomersController? _sut;
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

    //Sad Path - Checking that if a customer already exists with a given ID, the database will not be updated
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

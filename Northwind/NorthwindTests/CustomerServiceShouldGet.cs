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

public partial class CustomerServiceShould
{
    private CustomerService _sut;

    
    
    [Test]
    public void ReturnACustomer_When_GetCustomerByIdAsyncIsCalledWithAValidId()
    {
        //_sut.GetCustomerByIdAsync(customer)
        var result = _customerService.GetCustomerByIdAsync("MANDA");

        Assert.That(result, Is.InstanceOf<Task<Customer>>());
    }

    [Test]
    public void ReturnAListofCustomers_When_GetCustomerListAsyncIsCalledWithAValidId()
    {
        //_sut.GetCustomerByIdAsync(customer)
        var result = _customerService.GetCustomerListAsync();

        Assert.That(result, Is.InstanceOf<Task<List<Customer>>>());
    }

}

using Moq;
using NorthwindApi.Models;
using NorthwindApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NorthwindTests;

public partial class CustomerServiceShould
{
    private ICustomerService _sut;

    //happy path
    [Test]
    public void AListOfCustomers_WhenGetCustomersIsCalled()
    {
        var mockContext = new Mock<NorthwindContext>();
        var customer = new Customer() { CustomerId = 1}
        _sut = new CustomerService(mockContext.Object);


    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NorthwindApi.Controllers;
using NorthwindApi.Models;
using NorthwindApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTests;

public partial class CustomerControllerShouldGet
{
    private CustomersController? _sut;

    [Category("Happy Path")]
    [Test]
    public void BeAbleToBeConstructed()
    {
        var mockService = new Mock<ICustomerService>();
        _sut = new CustomersController(mockService.Object);
        Assert.That(_sut, Is.InstanceOf<CustomersController>());
    }

    [Category("Happy Path")]
    [Test]
    public void ReturnATaskActionResultListCustomer_WhenGetCustomersIsCalled()
    {
        var mockService = new Mock<ICustomerService>();
        _sut = new CustomersController(mockService.Object);
        var result = _sut.GetCustomers();
        Assert.That(result, Is.InstanceOf<Task<ActionResult<IEnumerable<Customer>>>>());
    }


}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NorthwindApi.Controllers;
using NorthwindApi.Models;
using NorthwindApi.Services;

namespace NorthwindTests;

public class CustomerControllerShouldPut
{
    private CustomersController? _sut;
    private Mock<ICustomerService> mockService;

    [SetUp]
    public void SetUp()
    {
        mockService = new Mock<ICustomerService>();
        _sut = new CustomersController(mockService.Object);
    }

    [Test]
    public void BeAbleToBeConstructed()
    {
        Assert.That(_sut, Is.InstanceOf<CustomersController>());
    }

    [Category("Happy Path")]
    [Test]
    public void ReturnNoContentResult_WhenUpdateIsCalled_WithValidId()
    {
        // Arrange
        var mockCustomerService = new Mock<ICustomerService>();
        var originalCustomer = new Customer { CustomerId = "MANDA", ContactName = "Nish Mandal", City = "Brum", Country = "UK", CompanyName = "Nintendo" };
        mockCustomerService
            .Setup(cs => cs.GetCustomerByIdAsync("MANDA"))
            .ReturnsAsync(originalCustomer);
        _sut = new CustomersController(mockCustomerService.Object);

        // Act
        var result = _sut.PutCustomer("MANDA", new Customer { CustomerId = "MANDA", ContactName = "Ijaz Sadiq Basha" }).Result;

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>());
    }

    [Category("Sad Path")]
    [Test]
    public void ReturnNoContentResult_WhenUpdateIsCalled_WithInvalidId()
    {
        // Arrange
        var mockCustomerService = new Mock<ICustomerService>(MockBehavior.Loose);
        mockCustomerService
            .Setup(cs => cs.GetCustomerByIdAsync("MANDA"))
            .ReturnsAsync((Customer)null);
        _sut = new CustomersController(mockCustomerService.Object);

        // Act
        var result = _sut.PutCustomer("MANDA", new Customer { CustomerId = "MANDA", ContactName = "Ijaz Sadiq Basha" }).Result;

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Category("Sad Path")]
    [Test]
    public void ReturnBadRequestResult_WhenUpdateIsCalled_WithInvalidIdInCustomer()
    {
        // Arrange
        var mockCustomerService = new Mock<ICustomerService>();
        var originalCustomer = new Customer { CustomerId = "MANDA", ContactName = "Nish Mandal", City = "Brum", Country = "UK", CompanyName = "Nintendo" };
        mockCustomerService
            .Setup(cs => cs.GetCustomerByIdAsync("MANDA"))
            .ReturnsAsync(originalCustomer);
        _sut = new CustomersController(mockCustomerService.Object);

        // Act
        var result = _sut.PutCustomer("MANDA", new Customer { ContactName = "Ijaz Sadiq Basha" }).Result;

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestResult>());
    }

}

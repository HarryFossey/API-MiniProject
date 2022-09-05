using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NorthwindApi.Controllers;
using NorthwindApi.Models;
using NorthwindApi.Services;

namespace NorthwindTests
{
    public class CustomerManagerShouldPost
    {
        private CustomersController? _sut;


        //Happy
        [Test]
        public void VerifyThat_SaveCustomerChangesAsync_IsCalledWithValidCustomer()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(ms => ms.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(new Customer());
            _sut = new CustomersController(mockService.Object);
            _sut.PostCustomer(new Customer());
            mockService.Verify(ms => ms.SaveCustomerChangesAsync(), Times.Once);
        }

        [Test]
        public void VerifyThat_CreateCustomerAsync_IsNotCalledWithInvalidCustomer()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(ms => ms.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync((Customer)null);
            _sut = new CustomersController(mockService.Object);
            _sut.PostCustomer(new Customer());
            mockService.Verify(ms => ms.CreateCustomerAsync(new Customer()), Times.Never);
        }
    }
    
}

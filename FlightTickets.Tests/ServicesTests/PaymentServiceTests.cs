using FlightTickets.PaymentAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTickets.Tests.ServicesTests
{
    public class PaymentServiceTests
    {
        public readonly PaymentService _paymentService;

        public PaymentServiceTests()
        {
            _paymentService = new PaymentService();
        }

        [Fact]
        public void GetTicketsFromQueueAsync_WithoutException()
        {
            var exception = Record.ExceptionAsync(() =>
            _paymentService.GetTicketsFromQueueAsync()).Result;

            Assert.Null(exception);
        }
    }
}

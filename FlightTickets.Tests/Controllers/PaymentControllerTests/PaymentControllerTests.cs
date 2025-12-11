using FlightTickets.PaymentAPI.Controllers;
using FlightTickets.PaymentAPI.Services;
using FlightTickets.PaymentAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTickets.Tests.Controllers.PaymentControllerTests
{
    public class PaymentControllerTests
    {
        public ILogger<PaymentController> _logger;

        public PaymentService _paymentService;
        public PaymentController _paymentController;

        public PaymentControllerTests()
        {
            _logger = new LoggerFactory().CreateLogger<PaymentController>();
            _paymentService = new PaymentService();
            _paymentController = new PaymentController(_logger, _paymentService);
        }

        [Fact]
        //[Trait("VerbosHTTP", "Get")] -> firulisse
        public void ProcessPaymentMustReturnOkResult()
        {
            //Act
            var result = _paymentController.Get().Result;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}

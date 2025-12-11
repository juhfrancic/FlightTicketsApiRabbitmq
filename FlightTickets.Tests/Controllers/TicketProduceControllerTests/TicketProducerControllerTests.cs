using Bogus;
using Bogus.DataSets;
using FlightTickets.Models.DTOs;
using FlightTickets.Models.Models;
using FlightTickets.OrderAPI.Controllers;
using FlightTickets.OrderAPI.Services;
using FlightTickets.PaymentAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTickets.Tests.Controllers.TicketServiceTests
{
    public class TicketProducerControllerTests
    {
        public ILogger<TicketController> _logger;
        public TicketService _ticketService;
        public TicketController _ticketController;

        public TicketProducerControllerTests()
        {
            _logger = new LoggerFactory().CreateLogger<TicketController>(); ;
            _ticketService = new TicketService();
            _ticketController = new TicketController(_logger, _ticketService);
        }

        [Fact]
        public void CreateTicketMustReturnOk()
        {
            // Arrange — criando o DTO usando Faker
            var faker = new Bogus.Faker();

            var ticket = new TicketRequestDTO
            {
                PassengerName = faker.Person.FullName,
                FlightNumber = Guid.NewGuid().ToString(),
                SeatNumber = faker.Random.String2(2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
                       + faker.Random.Number(1, 30),
                Price = faker.Random.Decimal(100, 2000)
            };

            // Act
            var result = _ticketController.CreateTicket(ticket).Result;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}


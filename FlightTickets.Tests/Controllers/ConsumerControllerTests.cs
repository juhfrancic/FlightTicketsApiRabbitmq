using Bogus;
using FlightTickets.ConsumerAPI.Controllers;
using FlightTickets.ConsumerAPI.Data;
using FlightTickets.ConsumerAPI.Repositories;
using FlightTickets.ConsumerAPI.Repositories.Interfaces;
using FlightTickets.ConsumerAPI.Services;
using FlightTickets.Models.Models;
using FlightTickets.PaymentAPI.Controllers;
using FlightTickets.PaymentAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTickets.Tests.Controllers
{
    public class ConsumerControllerTests
    {
        public ILogger<ConsumerController> _loggerController;
        public ILogger<ConsumerService> _loggerService;
        public ILogger<ConsumerRepository> _loggerRepository;
        public ConnectionDB _connection;
        public IConsumerRepository _consumerRepository;
        public ConsumerService _consumerService;
        public ConsumerController _consumerController;
        IOptions<MongoDbSettings> _mongoDbSettings;

        public ConsumerControllerTests()
        {
            _loggerController = new LoggerFactory().CreateLogger<ConsumerController>();
            _loggerService = new LoggerFactory().CreateLogger<ConsumerService>();
            _loggerRepository = new LoggerFactory().CreateLogger<ConsumerRepository>();
            Faker faker = new Faker("pt_BR");
            var settings = new MongoDbSettings
            {
                ConnectionURI = "mongodb://localhost:27017",
                DatabaseName = faker.Random.Word(),
                TicketApprovedCollection = faker.Random.Word(),
                TicketDeniedCollection = faker.Random.Word()
            };

            _mongoDbSettings = Options.Create(settings);
            _connection = new ConnectionDB(_mongoDbSettings);
            _consumerRepository = new ConsumerRepository(_loggerRepository, _connection);
            _consumerService = new ConsumerService(_loggerService, _consumerRepository);
            _consumerController = new ConsumerController(_loggerController, _consumerService);
        }

        [Fact]
        public void TicketSaveMustReturnOkResult()
        {
            // Act
            var result = _consumerController.TicketSaveDatabase().Result;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}

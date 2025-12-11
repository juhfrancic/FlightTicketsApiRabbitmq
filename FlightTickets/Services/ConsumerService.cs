using FlightTickets.ConsumerAPI.Repositories.Interfaces;
using FlightTickets.ConsumerAPI.Services.Interfaces;
using FlightTickets.Models.Models;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FlightTickets.ConsumerAPI.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerService(ILogger<ConsumerService> logger, IConsumerRepository consumerRepository)
        {
            _logger = logger;
            _consumerRepository = consumerRepository;
        }

        public async Task GetTicketsFromQueues()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: "TicketApproved",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var ticket = JsonSerializer.Deserialize<Ticket>(message);
                    await SaveApprovedTicketsToCollection(ticket);
                };

                await channel.BasicConsumeAsync(
                    queue: "TicketApproved",
                    autoAck: true,
                    consumer: consumer
                );
                await channel.QueueDeclareAsync(
                    queue: "TicketDenield",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var ticket = JsonSerializer.Deserialize<Ticket>(message);
                    await SaveDeniedTicketsToCollection(ticket);
                };

                await channel.BasicConsumeAsync(
                    queue: "TicketDenied",
                    autoAck: true,
                    consumer: consumer
                );
            }
            catch (Exception ex)
            {
                _logger.LogError("Shit happens...(GetTikets)" + ex.Message);
            }
        }

        public async Task SaveApprovedTicketsToCollection(Ticket ticket)
        {
            try
            {
                await _consumerRepository.SaveApprovedTicketsasync(ticket);
            }
            catch(Exception ex)
            {
                _logger.LogError("Shit happens...(SavedApprovedTickets)" + ex.Message);
            }
        }

        public async Task SaveDeniedTicketsToCollection(Ticket ticket)
        {
            try
            {
                await _consumerRepository.SaveDeniedTicketsAsync(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shit happens...(SavedDeniedTickets)" + ex.Message);
            }
        }
    }
}

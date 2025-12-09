using FlightTickets.Models.DTOs;
using FlightTickets.Models.Models;
using FlightTickets.OrderAPI.Services.Interfaces;
using RabbitMQ.Client;
using System.Text.Json;
using System.Threading.Channels;

namespace FlightTickets.OrderAPI.Services
{
    public class TicketService : ITicketService
    {
        public async Task<TicketResponseDTO> CreateTicketAsync(TicketRequestDTO ticketRequest)
        {
            try
            {
                var newTicket = new Ticket
                (
                    ticketRequest.PassengerName,
                    ticketRequest.FlightNumber,
                    ticketRequest.SeatNumber,
                    ticketRequest.Price
                );

                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();

                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: "TicketOrderQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var ticketString = JsonSerializer.Serialize(newTicket);
                
                var queueTicket = System.Text.Encoding.UTF8.GetBytes(ticketString);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "TicketOrderQueue",
                    body: queueTicket);

                return new TicketResponseDTO
                {
                    Id = newTicket.Id.ToString(),
                    PassengerName = newTicket.PassengerName,
                    FlightNumber = newTicket.FlightNumber,
                    SeatNumber = newTicket.SeatNumber,
                    Price = newTicket.Price
                };
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while creating the ticket.", ex);
            }
        }
    }
}

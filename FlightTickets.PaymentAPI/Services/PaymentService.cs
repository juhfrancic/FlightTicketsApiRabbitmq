using FlightTickets.Models.Models;
using FlightTickets.PaymentAPI.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FlightTickets.PaymentAPI.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task GetTicketsFromQueueAsync()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" }; 
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: "TicketOrderQueue",
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
                    await ValidatePaymentTicket(ticket);
                };

                await channel.BasicConsumeAsync(
                    queue: "TicketOrderQueue",
                    autoAck: true,
                    consumer: consumer
                );
            } 
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task ValidatePaymentTicket(Ticket ticket)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            if (ticket.Price > 1000)
            {

                await channel.QueueDeclareAsync(
                    queue: "TicketApproved",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
                var ticketString = JsonSerializer.Serialize(ticket);
                var body = Encoding.UTF8.GetBytes(ticketString);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "TicketApproved",
                    body: body
                );
            }
            else
            {
                await channel.QueueDeclareAsync(
                    queue: "TicketDenied",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );
                var ticketString = JsonSerializer.Serialize(ticket);
                var body = Encoding.UTF8.GetBytes(ticketString);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "TicketDenied",
                    body: body
                );
            }

        }
    }
}

using RabbitMQ.Client;

namespace FlightTickets.PaymentAPI.Data
{
    public interface IConnectionRabbitMqFactory
    {
        Task<IConnection> CreateConnectionAsync();
    }
}

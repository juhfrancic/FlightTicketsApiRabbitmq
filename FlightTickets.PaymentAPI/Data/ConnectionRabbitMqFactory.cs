using RabbitMQ.Client;

namespace FlightTickets.PaymentAPI.Data
{
    public class ConnectionRabbitMqFactory : IConnectionRabbitMqFactory
    {
        public async Task<IConnection> CreateConnectionAsync()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            return await factory.CreateConnectionAsync();
        }
    }
}

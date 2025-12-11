using FlightTickets.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlightTickets.ConsumerAPI.Data
{
    public class ConnectionDB
    {
        public readonly IMongoCollection<Ticket> TicketApprovedCollection;
        public readonly IMongoCollection<Ticket> TicketDeniedCollection;

            public ConnectionDB(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            TicketApprovedCollection = database.GetCollection<Ticket>(mongoDbSettings.Value.TicketApprovedCollection);
            TicketDeniedCollection = database.GetCollection<Ticket>(mongoDbSettings.Value.TicketDeniedCollection);
        }

        public IMongoCollection<Ticket> GetMongoApprovedCollection()
        {
            return TicketApprovedCollection;
        }
        public IMongoCollection<Ticket> GetMongoDeniedCollection()
        {
            return TicketDeniedCollection;
        }

    }
}

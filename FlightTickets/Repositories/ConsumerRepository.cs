using FlightTickets.ConsumerAPI.Data;
using FlightTickets.ConsumerAPI.Repositories.Interfaces;
using FlightTickets.Models.Models;
using MongoDB.Driver;

namespace FlightTickets.ConsumerAPI.Repositories
{
    public class ConsumerRepository : IConsumerRepository
    {
        private readonly ILogger<ConsumerRepository> _logger;
        private readonly IMongoCollection<Ticket> _collectionApproved;
        private readonly IMongoCollection<Ticket> _collectionDenied;
        public ConsumerRepository(ILogger<ConsumerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _collectionApproved = connection.GetMongoApprovedCollection();
            _collectionDenied = connection.GetMongoApprovedCollection();
        }
        public async Task SaveApprovedTicketsasync(Ticket ticket)
        {
            try
            {
                _logger.LogInformation("Saving approved ticket to database...");
                await _collectionApproved.InsertOneAsync(ticket);
            }
            catch(Exception ex) 
            {
                _logger.LogError("Shit happens...");
            }
        }

        public async Task SaveDeniedTicketsAsync(Ticket ticket)
        {
            try
            {
                _logger.LogInformation("Saving approved ticket to database...");
                await _collectionDenied.InsertOneAsync(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError("Shit happens...");
            }
        }
    }
}

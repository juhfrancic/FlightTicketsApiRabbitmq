using FlightTickets.Models.Models;

namespace FlightTickets.ConsumerAPI.Repositories.Interfaces
{
    public interface IConsumerRepository
    {
        Task SaveApprovedTicketsasync(Ticket ticket);
        Task SaveDeniedTicketsAsync(Ticket ticket);
    }
}

using FlightTickets.Models.Models;
using System.Net.Sockets;

namespace FlightTickets.ConsumerAPI.Services.Interfaces
{
    public interface IConsumerService
    {
        Task GetTicketsFromQueues();
        Task SaveApprovedTicketsToCollection(Ticket ticket);
        Task SaveDeniedTicketsToCollection(Ticket ticket);
    }
}

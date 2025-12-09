using FlightTickets.Models.DTOs;

namespace FlightTickets.OrderAPI.Services.Interfaces
{
    public interface ITicketService
    {
        Task<TicketResponseDTO> CreateTicketAsync(TicketRequestDTO ticketRequest);
    }
}

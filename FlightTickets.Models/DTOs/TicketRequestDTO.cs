using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTickets.Models.DTOs
{
    public class TicketRequestDTO
    {
        public string PassengerName { get; set; }
        public string FlightNumber { get; set; }
        public string SeatNumber { get; set; }
        public decimal Price { get; set; }  
    }
}

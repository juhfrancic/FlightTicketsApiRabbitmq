using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTickets.Models.DTOs
{
    public class TicketResponseDTO
    {
        [BsonId]
        public string Id { get; init; }
        public string PassengerName { get; init; }
        public string FlightNumber { get; init; }
        public string SeatNumber { get; init; }
        public decimal Price { get; init; }
    }
}


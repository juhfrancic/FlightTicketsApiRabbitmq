namespace FlightTickets.ConsumerAPI.Data
{
    public class MongoDbSettings
    {
        public string ConnectionURI { get; set; } 
        public string DatabaseName { get; set; }
        public string TicketApprovedCollection { get; set; }
        public string TicketDeniedCollection { get; set; }
    }
}

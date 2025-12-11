using Bogus;
using FlightTickets.Models.Models;

namespace FlightTickets.Tests.Models
{
    public class TicketModelTests
    {
        private Faker _faker = new Faker("pt_BR");
        [Fact]
        public void Test1()
        {
            //Arrage
            var passengerName = _faker.Name.FullName();
            var flightNumber = _faker.Random.AlphaNumeric(6).ToUpper();
            var seatNumber = $"{_faker.Random.Int(1, 30)}{_faker.Random.Char('A', 'F')}";
            var price = _faker.Finance.Amount(600, 6000);

            //Act
            var ticket = new Ticket(passengerName, flightNumber, seatNumber, price);

            //Assert
            Assert.NotNull(ticket);
            Assert.Equal(passengerName, ticket.PassengerName);
            Assert.Equal(flightNumber, ticket.FlightNumber);
            Assert.Equal(seatNumber, ticket.SeatNumber);
            Assert.Equal(price, ticket.Price);
        }
    }
}

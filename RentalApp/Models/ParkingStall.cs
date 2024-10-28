namespace RentalApp.Models
{
    public class ParkingStall
    {

        public int StallId { get; private set; }
        public bool Availability { get; private set; }
        public string Term { get; private set; }
        public string Type { get; private set; }
        public double Price { get; private set; }

        public ParkingStall(int stallId, bool availa, string term, string type, double price)
        {
            StallId = stallId;
            Availability = availa;
            Term = term;
            Type = type; // added a type to all for stall grouping. Also could be indoor / outdoor
            Price = price;
        }


    }
}

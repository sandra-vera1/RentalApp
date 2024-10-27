namespace RentalApp.Models
{
    public class Renter : User
    {

        public List<Quote> Quotes { get; set; } = new List<Quote>();


        public double GetNewQuotedPrice(Property prop)
        {

        }
    }
}

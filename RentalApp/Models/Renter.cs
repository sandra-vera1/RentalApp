namespace RentalApp.Models
{
    public class Renter : User
    {
        //i added contructor
        public Renter(int userId, string firstName, string lastName, string email, string phone, int age)
            : base(userId, firstName, lastName, email, phone, age) 
        {
        }

        //I comment bc error
        //public double GetNewQuotedPrice(Property prop)
        //{

        //}
    }
}

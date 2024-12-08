
using System.Data.SqlClient;
using System.Data;

namespace RentalApp.Models
{
    public class Property
    {
        // private readonly string _connectionString;
        // these all need to be public in order to use asp-for
        public int PropertyId { get;  set; }
        
        public int AddressId { get; set; }//added public
        public Address Address { get; set; } // to store address values ??
        public double SquareFootage { get; set; }
        public string Facilities { get; set; }
        public string Type { get; set; }
        public bool Availability { get; set; }
        public double Price { get; set; }
        public int TermId { get; set; }
        public int OwnerId { get; set; }

        // empty constructor is require to use asp-for
        public Property()
        {
        }
        public Property(int addressId, double sqFt, string facilities, int termId,
            string type, bool availa, double price, int ownerId)

        {
            // PropertyId = propertyId;
            AddressId = addressId;
            SquareFootage = sqFt;
            Facilities = facilities;
            TermId = termId;
            Type = type;
            Availability = availa;
            Price = price;
            OwnerId = ownerId;
        }

        public void SetPropertyId(int id)
        {
            PropertyId = id;
        }


    }
}

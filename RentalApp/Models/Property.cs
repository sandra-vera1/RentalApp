
using System.Data.SqlClient;
using System.Data;

namespace RentalApp.Models
{
    public class Property
    {
        private readonly string _connectionString;


        public int PropertyId { get; private set; }
        public int AddressId { get; private set; }//added public
        public double SquareFootage { get; private set; }
        public string Facilities { get; private set; }
        public int TermId {  get; private set; }
        public string Type {  get; private set; }
        public bool Availability { get; private set; }
        public double Price { get; private set; }

        public int OwnerId { get; private set; }




        public Property(int addressId, double sqFt, string facilities, int termId, 
            string type, bool availa, double price, int ownerId ) 

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

        public Property(string connectionString)
        {
            _connectionString = connectionString;

        }


        public bool CreateProperty(Property property)
        {
            int transaction = 0;
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("CreateProperty", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SqFt", SqlDbType.VarChar).Value = property.SquareFootage;
                    cmd.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = property.Facilities;
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = property.Type;

                    cmd.Parameters.Add("@Price", SqlDbType.Money).Value = property.Price;

                    cmd.Parameters.Add("@OwnerID", SqlDbType.Int).Value = property.OwnerId;
                    cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = property.AddressId;
                    cmd.Parameters.Add("@TermID", SqlDbType.Int).Value = property.TermId;

                    cmd.Parameters.Add("@Availability", SqlDbType.Bit).Value = property.Availability;

                   

                    con.Open();
                    transaction = cmd.ExecuteNonQuery();


                }
            }
            return transaction == 1;
        }


        // I believe this was so that users could check the cost based on a duration at a property
        // if the term is monthly and the renter would like to check a 6 month rental period then
        // Price * Monthly * 6
        // Unless we were intending a different use for this function
        //public double CalculateTotalRentalCost(int duration)
        //{
        //    return Price * Term * duration;
        //}



    }
}

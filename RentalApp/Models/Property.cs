
using System.Data.SqlClient;
using System.Data;

namespace RentalApp.Models
{
    public class Property
    {
        // private readonly string _connectionString;

        public int PropertyId { get; private set; }
        // these all need to be public in order to use asp-for
        public int AddressId { get; set; }//added public
        public Address Address { get; set; } // to store address values ??
        public double SquareFootage { get; set; }
        public string Facilities { get; set; }
        public string Type { get; set; }
        public bool Availability { get; set; }
        public double Price { get; set; }
        public int TermId { get; set; }
        public int OwnerId { get; private set; }

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

        //public Property(string connectionString)
        //{
        //    _connectionString = connectionString;

        //}


        //public bool CreateProperty(Property property)
        //{
        //    int transaction = 0;
        //    using (SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("CreateProperty", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@SqFt", SqlDbType.VarChar).Value = property.SquareFootage;
        //            cmd.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = property.Facilities;
        //            cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = property.Type;

        //            cmd.Parameters.Add("@Price", SqlDbType.Money).Value = property.Price;

        //            cmd.Parameters.Add("@OwnerID", SqlDbType.Int).Value = property.OwnerId;
        //            cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = property.AddressId;
        //            cmd.Parameters.Add("@TermID", SqlDbType.Int).Value = property.TermId;

        //            cmd.Parameters.Add("@Availability", SqlDbType.Bit).Value = property.Availability;



        //            con.Open();
        //            transaction = cmd.ExecuteNonQuery();


        //        }
        //    }
        //    return transaction == 1;
        //}


        //public List<Property> ListProperty()
        //{
        //    List<Property> properties = new List<Property>();
        //    // get a list of  all properties
        //    using (SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("DisplayProperties", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                // Property data
        //                double SqFt = Convert.ToDouble(dr.GetString("SqFt"));
        //                string Facilities = dr.GetString("Facilities");
        //                string Type = dr.GetString("Type");
        //                double Price = Convert.ToDouble(dr.GetDecimal("Price"));
        //                int OwnerId = dr.GetInt32("OwnerID");
        //                int AddressId = dr.GetInt32("AddressID");
        //                bool Availability = dr.GetBoolean("Availability");
        //                Property prop = new Property(AddressId, SqFt, Facilities, TermId, Type, Availability, Price, OwnerId);
                        
        //                // Owner data
        //                //prop.OwnerName = dr.GetString("UserName");
        //                //prop.OwnerPhoneNum = dr.GetString("PhoneNumber");
        //                //prop.OwnerEmail = dr.GetString("Email");

        //                // Term data 
        //                prop.Term = dr.GetString("TermName");

        //                // Address data
        //                string Neigborhood = dr.GetString("Neighborhood");
        //                int StreetNumber = dr.GetInt32("StreetNumber");
        //                string StreetName = dr.GetString("StreetName");
        //                string City = dr.GetString("City");
        //                int ProvinceID = dr.GetInt32("ProvinceID");
        //                string ProvinceName = dr.GetString("ProvinceName");
        //                string Country = dr.GetString("Country");
        //                int SuiteNumber = dr.IsDBNull("SuiteNumber") ? 0 : dr.GetInt32("SuiteNumber");
        //                string PostalCode = dr.GetString("PostalCode");
        //                prop.Address = new Address(Neigborhood, StreetNumber, StreetName, City,
        //                    ProvinceID, Country, PostalCode, SuiteNumber);
        //                prop.Address.ProvinceName = ProvinceName;

        //                properties.Add(prop);

        //            }
        //        }

        //        // trying to add an Address object to each property to easily display prop details

        //    }


        //    return properties;
        //    // I believe this was so that users could check the cost based on a duration at a property
        //    // if the term is monthly and the renter would like to check a 6 month rental period then
        //    // Price * Monthly * 6
        //    // Unless we were intending a different use for this function
        //    //public double CalculateTotalRentalCost(int duration)
        //    //{
        //    //    return Price * Term * duration;
        //    //}



        //}
    }
}

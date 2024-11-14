using System.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;

namespace RentalApp.Models
{
    public class Address
    {
        public int AddressId { get; private set; } // pending put in Class diagram
        public string Neighborhood { get; private set; }
        public int StreetNumber { get; private set; }
        public string StreetName { get; private set; }
        public string City { get; private set; }
        public int Province { get; private set; }
        public string Country { get; private set; } // added this field pending put in Class diagram
        public string PostalCode { get; private set; }
        public int SuiteNumber { get; private set; }


        public Address(string neighborhood, int streetNum, string streetName, string city, int province, string country, string postalCode, int suiteNum)
        {
            //AddressId = id;
            Neighborhood = neighborhood;
            StreetNumber = streetNum;
            StreetName = streetName;
            City = city;
            Province = province;
            Country = country;
            PostalCode = postalCode;
            SuiteNumber = suiteNum;


        }
        
        public override string ToString()
        {
            return $"{StreetNumber} {StreetName}, {City}, {Province}, {PostalCode}"; //fix sintax error
        }

        //Connect to DB //step 1
        public Address(string connectionString) 
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;
        //Connect to DB

        //create address()//step 2 create stored procedure before. This is a bridge between Class fields and Stored Procedure
        public bool CreateAddress(Address address)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveAddress", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.Parameters.Add("@Neighborhood", SqlDbType.VarChar).Value = address.Neighborhood;
                    cmd.Parameters.Add("@StreetNumber", SqlDbType.Int).Value = address.StreetNumber;
                    cmd.Parameters.Add("@StreetName", SqlDbType.VarChar).Value = address.StreetName;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = address.City;
                    cmd.Parameters.Add("@ProvinceID", SqlDbType.Int).Value = address.Province;
                    cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = address.Country;
                    cmd.Parameters.Add("@SuiteNumber", SqlDbType.Int).Value = address.SuiteNumber;
                    cmd.Parameters.Add("@PostalCode", SqlDbType.Char).Value = address.PostalCode;
                    con.Open();
                    transaction = cmd.ExecuteNonQuery();
                }
            }
            return transaction == 1;
        }

         public void IndexAddress()
        {

           

            
        }


    }
}

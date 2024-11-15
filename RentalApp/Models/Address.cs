using System.Data.SqlClient;
using System.Data;

namespace RentalApp.Models
{
    public class Address
    {
        public int AddressId { get;  set; } // pending put in Class diagram
        public string Neighborhood { get; private set; }
        public int StreetNumber { get; private set; }
        public string StreetName { get; private set; }
        public string City { get; private set; }
        public int Province { get; private set; }
        public string Country { get; private set; } // added this field pending put in Class diagram
        public string PostalCode { get; private set; }
        public int SuiteNumber { get; private set; }

        public string ProvinceName { get; private set; }
        public List<Address> Provinces { get; set; }

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

        public Address(int province, string provinceName) { 

            Province = province;
            ProvinceName = provinceName;
        }

        //Connect to DB //step 1
        public Address(string connectionString)
        {
            _connectionString = connectionString;
            Provinces = ListProvinces();
        }
        public override string ToString()
        {
            return $"{StreetNumber} {StreetName}, {City}, {Province}, {PostalCode}"; //fix sintax error
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

        //[Update Address] //step 1 create stored procedure before. Connection is ready.
        public bool UpdateAddress(Address address)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateAddress", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = address.AddressId;
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







        // [DeleteAddress step 1] - create stored procedure before. Connection is ready.
        public bool DeleteAddress(int AddressId)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteAddress", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = AddressId;
                    con.Open();
                    transaction = cmd.ExecuteNonQuery();
                }
            }
            return transaction == 1;
        }


        // [Address List step 1] - create stored procedure before. Connection is ready.
        public List<Address> ListAddress()
        {
            List<Address> addresses = new List<Address>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DisplayAddresses", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string Neigborhood = dr.GetString("Neighborhood");
                        int StreetNumber = dr.GetInt32("StreetNumber");
                        string StreetName = dr.GetString("StreetName");
                        string City = dr.GetString("City");
                        int ProvinceID = dr.GetInt32("ProvinceID");
                        string Country = dr.GetString("Country");
                        int SuiteNumber = dr.IsDBNull("SuiteNumber") ? 0 : dr.GetInt32("SuiteNumber");
                        string PostalCode = dr.GetString("PostalCode");
                        Address address = new Address(Neigborhood, StreetNumber, StreetName, City,
                            ProvinceID, Country, PostalCode, SuiteNumber);
                        address.AddressId = dr.GetInt32("AddressID");
                        address.ProvinceName = dr.GetString("ProvinceName");
                        addresses.Add(address);
                    }
                }
            }
            return addresses;
        }

        public List<Address> ListProvinces()
        {
            List<Address> provinces = new List<Address>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DisplayProvinces", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        int ProvinceID = dr.GetInt32("ProvinceID");
                        string ProvinceName = dr.GetString("ProvinceName");
                        provinces.Add(new Address(ProvinceID, ProvinceName));
                    }
                }
            }
            return provinces;
        }

    }
}

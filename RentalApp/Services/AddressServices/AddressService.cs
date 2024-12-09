using RentalApp.Models;
using RentalApp.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace RentalApp.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        public bool CreateAddress(string connectionString, Address address)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveAddress", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = address.UserId;
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

        public bool DeleteAddress(string connectionString, int AddressId)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
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

        // returns a list of all addresses
        public IEnumerable<AddressViewModel> ListAddress(string connectionString)
        {
            List<AddressViewModel> addresses = new List<AddressViewModel>();
            List<Province> provinces = ListProvinces(connectionString);
            using (SqlConnection con = new SqlConnection(connectionString))
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
						address.UserId = dr.GetInt32("UserID");
						address.ProvinceName = dr.GetString("ProvinceName");
						address.Provinces = provinces;
                        AddressViewModel addressViewModel = new AddressViewModel(address);
                        addresses.Add(addressViewModel);
                    }
                }
            }
            return addresses;
        }

        public List<Province> ListProvinces(string connectionString)
        {
            List<Province> provinces = new List<Province>();
            using (SqlConnection con = new SqlConnection(connectionString))
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
                        provinces.Add(new Province(ProvinceID, ProvinceName));
                    }
                }
            }
            return provinces;
        }

        public bool UpdateAddress(string connectionString, Address address)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
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


        // returns a list of addresses belonging to a specific user ID. 
        public List<Address> GetAddressesOfUser(string connectionString, int userId)
        {
            List<Address> addresses = new List<Address>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAddressesOfUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
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
                        address.UserId = dr.GetInt32("UserID");
                        address.ProvinceName = dr.GetString("ProvinceName");
                        addresses.Add(address);
                    }
                }
            }
            return addresses;
        }


        // uses a given address Id to retrieve address details for that ID
        public Address GetAddressOfProperty(string connectionString, int addressId)
        {
            Address address = new Address();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Address a JOIN Provinces p ON p.ProvinceID = a.ProvinceID WHERE AddressID = @addressId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@addressId", SqlDbType.Int).Value = addressId;
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

                        address.Neighborhood = Neigborhood;
                        address.StreetNumber = StreetNumber;
                        address.StreetName = StreetName;
                        address.City = City;
                        address.Province = ProvinceID;
                        address.Country = Country;
                        address.SuiteNumber = SuiteNumber;
                        address.PostalCode = PostalCode;
                        address.AddressId = dr.GetInt32("AddressID");
                        address.UserId = dr.GetInt32("UserID");
                        address.ProvinceName = dr.GetString("ProvinceName");
                    }
                }
            }
            return address;
        }


    }
}

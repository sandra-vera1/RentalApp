using RentalApp.Models;
using System.Data.SqlClient;
using System.Data;
//using RentalApp.Services.PropertyServices;
using RentalApp.ViewModels;


// https://www.youtube.com/watch?v=Wiy54682d1w
// https://www.c-sharpcorner.com/article/implement-and-register-dependency-injection-in-asp-net-core-net-6/
// used these to learn about repository pattern and services
namespace RentalApp.Services.PropertyService
{
    public class PropertyService : IPropertyService
    {
        //private readonly string _connectionString;


		public IEnumerable<PropertyOwnerListView> Get(string connectionString)
		{
			List<PropertyOwnerListView> properties = new List<PropertyOwnerListView>();
			// get a list of  all properties
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("DisplayProperties", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					con.Open();
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{



						// Property data
						double SqFt = Convert.ToDouble(dr.GetString("SqFt"));
						string Facilities = dr.GetString("Facilities");
						string Type = dr.GetString("Type");
						double Price = Convert.ToDouble(dr.GetDecimal("Price"));
						int OwnerId = dr.GetInt32("OwnerID");
						int AddressId = dr.GetInt32("AddressID");
						bool Availability = dr.GetBoolean("Availability");
						int TermId = dr.GetInt32("TermID");
						Property prop = new Property(AddressId, SqFt, Facilities, TermId, Type, Availability, Price, OwnerId);

						// Address data
						string Neigborhood = dr.GetString("Neighborhood");
						int StreetNumber = dr.GetInt32("StreetNumber");
						string StreetName = dr.GetString("StreetName");
						string City = dr.GetString("City");
						int ProvinceID = dr.GetInt32("ProvinceID");
						string Country = dr.GetString("Country");
						int SuiteNumber = dr.IsDBNull("SuiteNumber") ? 0 : dr.GetInt32("SuiteNumber");
						string PostalCode = dr.GetString("PostalCode");
                        string ProvinceName = dr.GetString("ProvinceName");
                        prop.Address = new Address(Neigborhood, StreetNumber, StreetName, City,
							ProvinceID, Country, PostalCode, SuiteNumber);
						prop.Address.ProvinceName = ProvinceName;


                        // Owner data
                        string name = dr.GetString("FullName");
						string phoneNum = dr.GetString("PhoneNumber");
						string email = dr.GetString("Email");

						OwnerContactInfo owner = new OwnerContactInfo(name, phoneNum, email);


						PropertyOwnerListView propDetails = new PropertyOwnerListView(prop, owner);
						propDetails.Term = dr.GetString("TermName");
						propDetails.Province = ProvinceName;
						properties.Add(propDetails);

					}
				}

			}

			return properties;
		}

		public bool Create(string connectionString, Property property)
		{
			int transaction = 0;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("CreateProperty", con))
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
			Console.WriteLine("Create Success?!");
			return transaction == 1;

			//throw new NotImplementedException();
		}

        public IEnumerable<Property> Get()
        {
            throw new NotImplementedException();
        }
    }
}

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
						int PropertyId = dr.GetInt32("PropertyId");
						double SqFt = Convert.ToDouble(dr.GetString("SqFt"));
						string Facilities = dr.GetString("Facilities");
						string Type = dr.GetString("Type");
						double Price = Convert.ToDouble(dr.GetDecimal("Price"));
						int OwnerId = dr.GetInt32("OwnerID");
						int AddressId = dr.GetInt32("AddressID");
						bool Availability = dr.GetBoolean("Availability");
						int TermId = dr.GetInt32("TermID");
						Property prop = new Property(AddressId, SqFt, Facilities, TermId, Type, Availability, Price, OwnerId);
						prop.SetPropertyId(PropertyId);

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


		public Property GetPropertyById(string connectionString, int id)
		{
			Property property = new Property();
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();
				string query = "SELECT PropertyID, SqFt, Facilities, Type, Price, AddressID, TermID, Availability FROM Properties WHERE PropertyID = @propertyID";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@propertyID", id);
					SqlDataReader result = cmd.ExecuteReader();

					while (result.Read())
					{
						property.PropertyId = Convert.ToInt32(result["PropertyID"]);
						property.SquareFootage = Convert.ToDouble(result["SqFt"]);
						property.Facilities = result["Facilities"].ToString();
						property.Type = result["Type"].ToString();
						property.Price = Convert.ToDouble(result["Price"]);
						property.AddressId = Convert.ToInt32(result["AddressID"]);
						property.TermId = Convert.ToInt32(result["TermID"]);
						property.Availability = result.GetBoolean("Availability");
					}

				}
			}

			return property;
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


		public bool Edit(string connectionString, Property property, int propertyID)
		{
			int transaction = 0;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("UpdateProperty", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@PropertyId", SqlDbType.Int).Value = propertyID;
					cmd.Parameters.Add("@SqFt", SqlDbType.VarChar).Value = property.SquareFootage;
					cmd.Parameters.Add("@Facilities", SqlDbType.VarChar).Value = property.Facilities;
					cmd.Parameters.Add("@Type", SqlDbType.VarChar).Value = property.Type;

					cmd.Parameters.Add("@Price", SqlDbType.Money).Value = property.Price;


					cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = property.AddressId;
					cmd.Parameters.Add("@TermID", SqlDbType.Int).Value = property.TermId;

					cmd.Parameters.Add("@Availability", SqlDbType.Bit).Value = property.Availability;

					con.Open();
					transaction = cmd.ExecuteNonQuery();
				}
			}
			Console.WriteLine("Edit Success?!");
			return transaction == 1;


		}
		public IEnumerable<Property> Get()
		{
			throw new NotImplementedException();
		}

		public bool CreateFavorite(string connectionString, int PropertyID, int RenterID)
		{
			int transaction = 0;
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("CreateFavoriteP", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = PropertyID;
					cmd.Parameters.Add("@RenterID", SqlDbType.Int).Value = RenterID;

					con.Open();
					transaction = cmd.ExecuteNonQuery();
				}
			}
			return transaction == 1;
		}

		public bool DeleteFavorite(string connectionString, int FavoriteID)
		{
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteFavoriteP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FavoriteID", SqlDbType.Int).Value = FavoriteID;

                    con.Open();
                    transaction = cmd.ExecuteNonQuery();
                }
            }
            return transaction == 1;
        }

		public IEnumerable<PropertyFavoriteListView> GetFavoriteList(string connectionString, int RenterID)
		{
			List<PropertyFavoriteListView> FavoriteList = new List<PropertyFavoriteListView>();

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("DisplayFavoriteListWithOwners", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@RenterID", SqlDbType.Int).Value = RenterID;
					con.Open();
					SqlDataReader dr = cmd.ExecuteReader();
					while (dr.Read())
					{
						PropertyFavoriteListView property = new PropertyFavoriteListView();

						property.FavoriteId = dr.GetInt32("Id");
						property.PropertyID = dr.GetInt32("PropertyId");
						property.SqFt = dr.GetString("SqFt");
						property.Facilities = dr.GetString("Facilities");
						property.Type = dr.GetString("Type");
						property.Price = dr.GetDecimal("Price");
						property.Availability = dr.GetBoolean("Availability") ? "Yes" : "No";
						property.TermName = dr.GetString("TermName");
						property.FullName = dr.GetString("FullName");
						property.PhoneNumber = dr.GetString("PhoneNumber");
						property.Email = dr.GetString("Email");
						property.Neighborhood = dr.GetString("Neighborhood");
						property.StreetNumber = dr.GetInt32("StreetNumber").ToString();
						property.StreetName = dr.GetString("StreetName");
						property.City = dr.GetString("City");
						property.ProvinceName = dr.GetString("ProvinceName");
						property.Country = dr.GetString("Country");
						property.SuiteNumber = dr.IsDBNull("SuiteNumber") ? "" : dr.GetInt32("SuiteNumber").ToString();
						property.PostalCode = dr.GetString("PostalCode");
						FavoriteList.Add(property);

					}
				}
			}
			return FavoriteList;
		}

        public QuoteViewModel GetPropertyQuote(string connectionString, int PropertyId)
        {
            QuoteViewModel quoteViewModel = new QuoteViewModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("QuotedInformation", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = PropertyId;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        quoteViewModel.PropertyID = dr.GetInt32("PropertyId");
                        quoteViewModel.SqFt = dr.GetString("SqFt");
                        quoteViewModel.Facilities = dr.GetString("Facilities");
                        quoteViewModel.Type = dr.GetString("Type");
                        quoteViewModel.Price = Convert.ToDouble(dr.GetDecimal("Price"));
                        quoteViewModel.Availability = dr.GetBoolean("Availability") ? "Yes" : "No";
						quoteViewModel.TermID = dr.GetInt32("TermID");
                        quoteViewModel.TermName = dr.GetString("TermName");
                        quoteViewModel.FullName = dr.GetString("FullName");
                        quoteViewModel.PhoneNumber = dr.GetString("PhoneNumber");
                        quoteViewModel.Email = dr.GetString("Email");
                        quoteViewModel.Neighborhood = dr.GetString("Neighborhood");
                        quoteViewModel.StreetNumber = dr.GetInt32("StreetNumber").ToString();
                        quoteViewModel.StreetName = dr.GetString("StreetName");
                        quoteViewModel.City = dr.GetString("City");
                        quoteViewModel.ProvinceName = dr.GetString("ProvinceName");
                        quoteViewModel.Country = dr.GetString("Country");
                        quoteViewModel.SuiteNumber = dr.IsDBNull("SuiteNumber") ? "" : dr.GetInt32("SuiteNumber").ToString();
                        quoteViewModel.PostalCode = dr.GetString("PostalCode");
						quoteViewModel.Terms = GetTerms(quoteViewModel.TermID);
                    }
                }
            }
            return quoteViewModel;
        }

		private List<int> GetTerms(int TermId)
		{
			switch (TermId)
            {
                case 1:
                    return LoopTerms(2, 5);
                case 2:
                    return LoopTerms(2, 6);
                case 3:
					return LoopTerms(10, 24);
                case 4:
                    return LoopTerms(60, 90);
                default:
					return new List<int>();
			}
		}

		private List<int> LoopTerms(int init, int end)
        {
			List<int> terms = new List<int>();
            for (int i = init; i <= end; i++)
            {
                terms.Add(i);
            }
			return terms;
        }
    }
}

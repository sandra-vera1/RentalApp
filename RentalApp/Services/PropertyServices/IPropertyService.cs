using RentalApp.Models;
using RentalApp.ViewModels;

namespace RentalApp.Services.PropertyService
{
    public interface IPropertyService
    {

		IEnumerable<PropertyOwnerListView> Get(string connectionString);
		public bool Create(string connectionString, Property property);
		public bool Edit(string connectionString, Property property, int propertyId);
        public bool Delete(string connectionString, int propertyId);

        public Property GetPropertyById(string connectionString, int propertyId);


        public bool CreateFavorite(string connectionString, int PropertyID, int RenterID);

        public bool DeleteFavorite(string connectionString, int FavoriteID);

        IEnumerable<PropertyFavoriteListView> GetFavoriteList(string connectionString, int RenterID);
        public QuoteViewModel GetPropertyQuote(string connectionString, int PropertyId);


        public List<Term> GetTerms(string connectionString);
        public string GetTermNameById(string connectionString, int id);

    }
}

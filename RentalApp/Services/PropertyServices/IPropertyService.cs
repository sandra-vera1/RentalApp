using RentalApp.Models;
using RentalApp.ViewModels;

namespace RentalApp.Services.PropertyService
{
    public interface IPropertyService
    {

		IEnumerable<PropertyOwnerListView> Get(string connectionString);
		public bool Create(string connectionString, Property property);


	}
}

using RentalApp.Models;

namespace RentalApp.ViewModels
{
    public class PropertyFavoriteListView
    {
        public int FavoriteId { get; set; }
        public int PropertyID { get; set; }
        public string SqFt { get; set; }
        public string Facilities { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Availability { get; set; }
        public string TermName { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Neighborhood { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string ProvinceName { get; set; }
        public string Country { get; set; }
        public string SuiteNumber { get; set; }
        public string PostalCode { get; set; }
    }
}

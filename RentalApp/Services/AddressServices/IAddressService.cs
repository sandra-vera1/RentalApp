using RentalApp.Models;
using RentalApp.ViewModels;

namespace RentalApp.Services.AddressServices
{
    public interface IAddressService
    {
        public bool CreateAddress(string connectionString, Address address);
        public bool DeleteAddress(string connectionString, int AddressId);
        public IEnumerable<AddressViewModel> ListAddress(string connectionString);
        public List<Province> ListProvinces(string connectionString);
        public bool UpdateAddress(string connectionString, Address address);

        public List<Address> GetAddressesOfUser(string connectionString, int userId);

        public Address GetAddressOfProperty(string connectionString, int addressId);
    }
}

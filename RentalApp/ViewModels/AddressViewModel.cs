using RentalApp.Models;

namespace RentalApp.ViewModels
{
    public class AddressViewModel
    {
        public Address address { get; set; }

        public AddressViewModel(Address address) {
            this.address = address;
        }
    }
}

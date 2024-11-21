

using RentalApp.Models;

namespace RentalApp.ViewModels
{
    public class PropertyOwnerListView
    {

        public Property Property { get; set; } 
        public OwnerContactInfo Owner {  get; set; }

        public string Term { get; set; }

        public string Province { get; set; }

        // public Address Address { get; set; }

        public PropertyOwnerListView()
        {

        }

        public PropertyOwnerListView(Property prop, OwnerContactInfo owner)
        {
            Property = prop;
            Owner = owner;
        }

    }
}

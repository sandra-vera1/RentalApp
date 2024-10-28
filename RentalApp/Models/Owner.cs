namespace RentalApp.Models
{
    public class Owner : User
    {






        public void AddProperty(int propertyId, int addressId, string neighborhood, int streetNum, string streetName,
            string city, string province, string postalCode,
            int suiteNum, double sqFt, string term,
            string type, bool availa, double price)
        {
            Properties.Add(new Property(propertyId, addressId, neighborhood, streetNum, streetName, 
                city, province, postalCode, 
                suiteNum, sqFt, term, 
                type, availa, price))
        }

        public void UpdateProperty(int id, Property updatedPropValues)
        {
            // would the easiest way of sending all values of a new property be to pass a temp property as the parameter
            // otherwise we have to add a LOT of arguments to this update function as well
            Properties.Find(prop => prop.PropertyId === id)
            {
                prop.PropertyAddress = updatedPropValues.Address;
                prop.SquareFootage = updatedPropValues.SquareFootage;
                prop.Term = updatedPropValues.Term;
                prop.Type = updatedPropValues.Type;
                prop.Availability = updatedPropValues.Availability;
                prop.Price  = updatedPropValues.Price;

            }
        }

        public void DeleteProperty(int id)
        {
            int index = Properties.FindIndex(prop => prop.PropertyId === id);

            // think this is right? I couldn't test.
            Properties.Slice(index, 1);

        }

        
    }
}

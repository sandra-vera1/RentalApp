namespace RentalApp.Models
{
    public class Address
    {
        public int AddressId { get; private set; }
        public string Neighborhood { get; private set; }
        public int StreetNumber { get; private set; }
        public string StreetName { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string PostalCode { get; private set; }
        public int SuiteNumber { get; private set; }


        public Address(int id, string neighborhood, int streetNum, string streetName, string city, string province, string postalCode, int suiteNum)
        {
            AddressId = id;
            Neighborhood = neighborhood;
            StreetNumber = streetNum;
            StreetName = streetName;
            City = city;
            Province = province;
            PostalCode = postalCode;
            SuiteNumber = suiteNum;


        }
        
        public override void ToString()
        {
            return $"{StreetNumber} {StreetName}, {City}, {Province} {PostalCode}"
        }

    }
}

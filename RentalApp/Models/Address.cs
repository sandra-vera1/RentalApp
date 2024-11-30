namespace RentalApp.Models
{
    public class Address
    {
        public int AddressId { get;  set; } // pending put in Class diagram
        public string Neighborhood { get; private set; }
        public int StreetNumber { get; private set; }
        public string StreetName { get; private set; }
        public string City { get; private set; }
        public int Province { get; private set; }
        public string Country { get; private set; } // added this field pending put in Class diagram
        public string PostalCode { get; private set; }
        public int SuiteNumber { get; private set; }

        public string ProvinceName { get; set; } // had to change this set to public to modify it when getting prop data
        public List<Province> Provinces { get; set; }

        public Address(List<Province> provinces)
        {
            Provinces = provinces;
        }
        public Address(string neighborhood, int streetNum, string streetName, string city, int province, string country, string postalCode, int suiteNum)
        {
            //AddressId = id;
            Neighborhood = neighborhood;
            StreetNumber = streetNum;
            StreetName = streetName;
            City = city;
            Province = province;
            Country = country;
            PostalCode = postalCode;
            SuiteNumber = suiteNum;
        }

        public override string ToString()
        {
            return $"{StreetNumber} {StreetName}, {City}, {Province}, {PostalCode}"; //fix sintax error
        }

    }
}

namespace RentalApp.Models
{
    public class Address
    {
        public int AddressId { get;  set; } // pending put in Class diagram
        public string Neighborhood { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public int Province { get; set; }
        public string Country { get; set; } // added this field pending put in Class diagram
        public string PostalCode { get; set; }
        public int SuiteNumber { get; set; }

        public string ProvinceName { get; set; } // had to change this set to public to modify it when getting prop data

        public int UserId { get; set; }
        public List<Province> Provinces { get; set; }

        public Address() { }

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

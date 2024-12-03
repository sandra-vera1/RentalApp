using RentalApp.Models;

namespace RentalApp.ViewModels
{
	public class QuoteViewModel
	{
		
        public int PropertyID { get; set; }
        public string SqFt { get; set; }
        public string Facilities { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public string Availability { get; set; }

        public int TermID { get; set; }
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


        public int TermDuration { get; set; }

        public double DiscountPercent { get; set; }

        public double DiscountPrice { get; set; }

        public double SecurityDeposit { get; set; }
        public double LateFee { get; set; }

        public double Insurance { get; set; }


        public DateTime ExpiresBy { get; set; } 
        public List<int> Terms { get; set; }
        public string jsonObj { get; set; }


	}
}

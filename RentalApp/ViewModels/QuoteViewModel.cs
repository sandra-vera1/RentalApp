using RentalApp.Models;

namespace RentalApp.ViewModels
{
	public class QuoteViewModel
	{
		public string SqFt { get; set; }
		public string Facilities { get; set; }
		public string Type { get; set; }
		public string TermName { get; set; }
		public decimal Price { get; set; }
		public string Address {  get; set; }
		public string OwnerInformation { get; set; }
		public DateTime ExpiresBy { get; set; }
		public int RentalDuration { get; set; }
		public double Deposit { get; set; }
	}
}

namespace RentalApp.Models
{
    public class Quote
    {
        public string RenterName { get; set; }
        public Property Property { get; set; }
        public DateTime ExpiresBy { get; set; }
        public double TermPrice { get; set; }
        public double Deposit { get; set; }
        public string Term {  get; set; }
        public int RentalDuration { get; set; }
    }
}

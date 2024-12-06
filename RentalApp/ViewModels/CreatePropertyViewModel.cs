using RentalApp.Models;
#nullable disable


namespace RentalApp.ViewModels
{


    public class CreatePropertyViewModel
    {


        public int PropertyId {  get; set; }
        public Address Address { get; set; }//added public
        public int AddressId { get; set; }
        public List<Address> AddressList { get; set; } 
        public double SquareFootage { get; set; }
        public string Facilities { get; set; }
        public string Type { get; set; }
        public bool Availability { get; set; }
        public double Price { get; set; }
        public List<Term> TermList {  get; set; }
        public string Term { get; set; }
        public int TermId { get; set; }



    }
}

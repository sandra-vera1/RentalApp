﻿namespace RentalApp.Models
{
    public class Property
    {
        public int PropertyId { get; private set; }
        Address PropertyAddress { get; private set; }
        public double SquareFootage { get; private set; }
        public string[] FacilitiesProperty { get; private set; }
        public string Term {  get; private set; }
        public string Type {  get; private set; }
        public bool Availability { get; private set; }
        public double Price { get; private set; }


        public Property(int propertyId, int addressId, string neighborhood, int streetNum, string streetName, 
            string city, string province, string postalCode, 
            int suiteNum, double sqFt,string term, 
            string type, bool availa, double price ) 

        { 
            PropertyId = id;
            PropertyAddress = new Address(addressId, neighborhood, streetNum, streetName, city, province, postalCode, suiteNum);
            SquareFootage = sqFt;
            Term = term;
            Type = type;
            Availability = availa;
            Price = price;

        }


        // I believe this was so that users could check the cost based on a duration at a property
        // if the term is monthly and the renter would like to check a 6 month rental period then
        // Price * Monthly * 6
        // Unless we were intending a different use for this function
        public double CalculateTotalRentalCost(int duration)
        {
            return Price * Term * duration;
        }



    }
}
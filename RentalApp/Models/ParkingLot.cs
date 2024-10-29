namespace RentalApp.Models
{
    public class ParkingLot
    {

        public List<ParkingStall> ParkingStalls = new List<ParkingStall>();

        // might need to change how we are tracking the id
        private int stallId = 1;


        // this function would be for large lots when most or all of the stalls are the same. 
        // Simply input the number of each type of stalls the parking lot has. 
        public void GenerateNumberOfStalls(int num, bool availa, string term, string type, double price)
        {

            for (int i = 0; i < num; i++) //hange 
            {
                ParkingStalls.Add(new ParkingStall(stallId, availa, term, type, price));
                stallId++;
            }
        }

        //i comment bc error
        //public double CalculateStallRentalCost(int id, int duration)
        //{

        //    ParkingStalls.Find(stall => stall.StallId == id);
        //    {
        //        return stall.Price * stall.Term * duration;
        //    }
        //}



    }
}

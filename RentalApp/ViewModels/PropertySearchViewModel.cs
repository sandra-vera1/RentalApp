namespace RentalApp.ViewModels
{
    public class PropertySearchViewModel
    {
        public string SearchAll { get; set; }
        public bool Availability { get; set; } = false;
        public double PriceMin { get; set; } = double.MinValue;
        public double PriceMax { get; set;} = double.MaxValue;
        public double SqFtMin { get; set; } = double.MinValue;
        public double SqFtMax { get; set; } = double.MaxValue;
        public string Neighborhood { get; set; } 
        public string Type { get; set; }


    }
}

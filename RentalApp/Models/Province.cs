namespace RentalApp.Models
{
    public class Province
    {
        public Province(int ProvinceId, string ProvinceName)
        {
            this.ProvinceId = ProvinceId;
            this.ProvinceName = ProvinceName;
        }
        public int ProvinceId { get; private set; }
        public string ProvinceName { get; set; } // had to change this set to public to modify it when getting prop data
    }
}

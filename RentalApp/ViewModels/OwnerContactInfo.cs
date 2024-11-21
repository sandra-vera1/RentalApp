namespace RentalApp.ViewModels
{
    public class OwnerContactInfo
    {
        public string UserName { get; set; }

        public string UserPhoneNumber { get; set; }

        public string UserEmail { get; set; }


        public OwnerContactInfo() { }
        public OwnerContactInfo(string name, string phoneNumber, string email)
        {
            UserName = name;
            UserPhoneNumber = phoneNumber;
            UserEmail = email;
        }
    }
}

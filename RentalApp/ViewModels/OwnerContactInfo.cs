namespace RentalApp.ViewModels
{
    public class OwnerContactInfo
    {
        public string FullName { get; set; }

        public string UserPhoneNumber { get; set; }

        public string UserEmail { get; set; }


        public OwnerContactInfo() { }
        public OwnerContactInfo(string name, string phoneNumber, string email)
        {
			FullName = name;
            UserPhoneNumber = phoneNumber;
            UserEmail = email;
        }
    }
}

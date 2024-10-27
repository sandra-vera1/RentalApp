namespace RentalApp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmail { get; set; }
        public int UserAccountType { get; set; }
        public Account Account { get; set; }
        public List<Property> Properties { get; set; }
        //protected List<>
        public User(int userId, string userName, string password, string userPhoneNumber, string userEmail, int userAccountType)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
            UserAccountType = userAccountType;
            Account = new Account(userName, password);
        }

    }
    //EditUser()
    //ViewAllProperties()
}

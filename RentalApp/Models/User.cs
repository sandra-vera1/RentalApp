namespace RentalApp.Models
{
    public class User
    {
		public int UserId { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmail { get; set; }
        public int UserAccountType { get; set; }

        public string UserAccountName { get; set; }

        public List<Property> Properties { get; set; }
        public List<User> Users { get; set; }

        public User() { }

        public User(string fullName, string userPhoneNumber, string userEmail)
        {
            FullName = fullName;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
        }


        public User(string fullName, string userPhoneNumber, string userEmail, int userAccountType)
        {
            FullName = fullName;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
            UserAccountType = userAccountType;
        }
        public User(string fullName, string password, string userPhoneNumber, string userEmail, int userAccountType)
        {
            FullName = fullName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
            UserAccountType = userAccountType;
        }

        

        public User(int userId, string fullName, string password, string userPhoneNumber, string userEmail, int userAccountType)
        {
			UserId = userId;
            FullName = fullName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
            UserAccountType = userAccountType;
        }


        
    }
}

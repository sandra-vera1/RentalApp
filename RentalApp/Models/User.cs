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

        public string UserAccountName { get; set; }

        public List<Property> Properties { get; set; }
        public List<User> Users { get; set; }


		public User(string userName, string password, string userPhoneNumber, string userEmail, int userAccountType)
        {
            UserName = userName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
            UserAccountType = userAccountType;
        }

        public User(string userName, string userPhoneNumber, string userEmail, int userAccountType)
        {
            UserName = userName;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
            UserAccountType = userAccountType;
        }

        public User(int userId, string userName, string password, string userPhoneNumber, string userEmail, int userAccountType)
        {
			UserId = userId;
            UserName = userName;
            Password = password;
            UserPhoneNumber = userPhoneNumber;
            UserEmail = userEmail;
            UserAccountType = userAccountType;
        }


        
    }
	
	//ViewAllProperties() ????
	//CheckCredentials(username,password)
	//GetMaskedPasword()

}

using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace RentalApp.Models
{
    public class User
    {
		private readonly string _connectionString;
		public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserEmail { get; set; }
        public int UserAccountType { get; set; }

        public string UserAccountName { get; set; }

        public List<Property> Properties { get; set; }
        public List<User> Users { get; set; }

        public User(string connectionString)
		{
			_connectionString = connectionString;
		}
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

        public bool CreateUser(User user)
		{
			int transaction = 0;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("SaveUser", con))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
					cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;
					cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = user.UserPhoneNumber;
					cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.UserEmail;
					cmd.Parameters.Add("@AccountTypeID", SqlDbType.Int).Value = user.UserAccountType;
					con.Open();
					transaction = cmd.ExecuteNonQuery();
				}
			}
			return transaction == 1;
		}

        public bool EditUser(User user)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserId;
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar).Value = user.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = user.UserPhoneNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = user.UserEmail;
                    cmd.Parameters.Add("@AccountTypeID", SqlDbType.Int).Value = user.UserAccountType;
                    con.Open();
                    transaction = cmd.ExecuteNonQuery();
                }
            }
            return transaction == 1;
        }

        public List<User> ListUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DisplayUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        string UserName = dr.GetString("UserName");
                        string Password = dr.GetString("Password");
                        string UserPhoneNumber = dr.GetString("PhoneNumber");
                        string UserEmail = dr.GetString("Email");
                        int UserAccountType = dr.GetInt32("AccountTypeID");
                        User user = new User(UserName, Password, UserPhoneNumber, UserEmail,
                            UserAccountType);
                        user.UserId = dr.GetInt32("UserID");
                        user.UserAccountName = dr.GetString("AccountName");
                        users.Add(user);
                    }
                }
            }
            return users;
        }


        
    }
	
	//ViewAllProperties() ????
	//CheckCredentials(username,password)
	//GetMaskedPasword()

}

using RentalApp.Models;
using System.Data.SqlClient;
using System.Data;
using RentalApp.ViewModels;

namespace RentalApp.Services.UserServices
{
    public class UserService : IUserService
    {
        public bool CreateUser(string connectionString, User user)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FullName", SqlDbType.VarChar).Value = user.FullName;
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

        public bool DeleteUser(string connectionString)
        {
            throw new NotImplementedException();
        }

        public bool EditUser(string connectionString, User user)
        {
            int transaction = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserId;
                    cmd.Parameters.Add("@FullName", SqlDbType.VarChar).Value = user.FullName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = user.Password;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = user.UserPhoneNumber;
                    cmd.Parameters.Add("@AccountTypeID", SqlDbType.Int).Value = user.UserAccountType;
                    con.Open();
                    transaction = cmd.ExecuteNonQuery();
                }
            }
            return transaction == 1;
        }

        public IEnumerable<UserViewModel> ListUsers(string connectionString)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DisplayUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        string FullName = dr.GetString("FullName");
                        string Password = dr.GetString("Password");
                        string UserPhoneNumber = dr.GetString("PhoneNumber");
                        string UserEmail = dr.GetString("Email");
                        int UserAccountType = dr.GetInt32("AccountTypeID");
                        User user = new User(FullName, Password, UserPhoneNumber, UserEmail,
                            UserAccountType);
                        user.UserId = dr.GetInt32("UserID");
                        user.UserAccountName = dr.GetString("AccountName");
                        users.Add(new UserViewModel(user));
                    }
                }
            }
            return users;
        }


        public OwnerContactInfo GetUserById(string connectionString, int userId)
        {

            OwnerContactInfo user = new OwnerContactInfo();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT FullName, PhoneNumber, Email FROM Users WHERE UserID = @userId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        user.FullName = dr.GetString("FullName");
                        user.UserPhoneNumber = dr.GetString("PhoneNumber");
                        user.UserEmail = dr.GetString("Email");
                    }
                }
            }
            return user;



        }
    }
}

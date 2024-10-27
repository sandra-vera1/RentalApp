using System.Data.SqlClient;
using System.Data;

namespace RentalApp.Models
{
    public class AppManager
    {
        public AppManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Property> Properties { get; set; }
        private readonly string _connectionString;
        List<User> users { get; set; }

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
    }
}

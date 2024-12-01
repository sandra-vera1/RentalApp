using RentalApp.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace RentalApp.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        public User CheckLogin(string connectionString, string userEmail, string password)
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CheckLogin", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserEmail", SqlDbType.VarChar).Value = userEmail;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
					{
						int UserID = dr.GetInt32("UserID");
						string FullName = dr.GetString("FullName");
                        string PhoneNumber = dr.GetString("PhoneNumber");
                        string Email = dr.GetString("Email");
                        int AccountTypeId = dr.GetInt32("AccountTypeId");
                        string AccountName = dr.GetString("AccountName");
						user = new User(FullName, PhoneNumber, Email, AccountTypeId)
						{
							UserAccountName = AccountName,
							UserId = UserID
						};
					}
                }
            }
            return user;
        }

        public ClaimsPrincipal CreateLogin(User autUser)
        {
            var claims = new List<Claim> {
                        new Claim("FullName", autUser.FullName),
                        new Claim("UserAccountName", autUser.UserAccountName),
                        new Claim("UserId", autUser.UserId.ToString()),
                        new Claim("UserEmail", autUser.UserEmail),
                        new Claim(ClaimTypes.Role, autUser.UserAccountName)
                    };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}

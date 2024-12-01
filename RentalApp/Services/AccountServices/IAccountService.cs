using RentalApp.Models;
using System.Security.Claims;

namespace RentalApp.Services.AccountServices
{
    public interface IAccountService
    {
        public User CheckLogin(string connectionString, string userEmail, string password);
        public ClaimsPrincipal CreateLogin(User autUser);
    }
}

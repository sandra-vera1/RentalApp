using RentalApp.Models;
using RentalApp.ViewModels;

namespace RentalApp.Services.UserServices
{
    public interface IUserService
    {
        public bool CreateUser(string connectionString, User user);

        public bool DeleteUser(string connectionString);

        public bool EditUser(string connectionString, User user);

        public IEnumerable<UserViewModel> ListUsers(string connectionString);

    }
}

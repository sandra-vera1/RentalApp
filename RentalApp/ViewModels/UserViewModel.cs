using RentalApp.Models;

namespace RentalApp.ViewModels
{
    public class UserViewModel
    {
        public User user { get; set; }

        public UserViewModel(User user) {
            this.user = user;
        }
    }
}

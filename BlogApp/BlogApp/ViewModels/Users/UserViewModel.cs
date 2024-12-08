using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Users
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserViewModel(User user)
        {
            User = user;
        }

    }
}

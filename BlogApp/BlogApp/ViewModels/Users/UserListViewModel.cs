using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Users
{
    public class UserListViewModel
    {
        public List<UserViewModel> Users { get; set; }=new List<UserViewModel>(){ };
    }
}

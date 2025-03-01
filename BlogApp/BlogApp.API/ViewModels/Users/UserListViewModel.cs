using BlogApp.Data.Models;

namespace BlogApp.API.ViewModels.Users;

public class UserListViewModel
{
    public List<User> Users { get; set; }=new List<User>(){ };
}

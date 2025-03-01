using BlogApp.Data.Models;

namespace BlogApp.ViewModels.Users;

public class UserViewModel
{
    public User User { get; set; }
    public UserViewModel(User user)
    {
        User = user;
    }
    public List<string> Roles { get; set; }= new List<string>();
    public List<Article> Articles { get; set; }= new List<Article>();
    public List<Comment> Comments { get; set; }= new List<Comment>();

}

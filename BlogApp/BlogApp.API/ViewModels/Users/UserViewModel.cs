using BlogApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.ViewModels.Users
{
    public class UserViewModel
    {
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string EmailReg { get; set; } = string.Empty;
		public DateTime BirthDate { get; set; }
		public string PasswordReg { get; set; } = string.Empty;
		public string PasswordConfirm { get; set; } = string.Empty;
		public string Login => EmailReg;
		public List<string> Roles { get; set; }= new List<string>();
        public List<Article> Articles { get; set; }= new List<Article>();
        public List<Comment> Comments { get; set; }= new List<Comment>();

    }
}

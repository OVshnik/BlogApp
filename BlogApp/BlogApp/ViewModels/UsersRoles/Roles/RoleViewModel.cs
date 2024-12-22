using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace BlogApp.ViewModels.UsersRoles.Roles
{
    public class RoleViewModel
    {
        public IdentityRole Role { get; set; }
        public bool IsChecked { get; set; }
        public RoleViewModel(IdentityRole role)
        {
            Role = role;
        }

    }
}

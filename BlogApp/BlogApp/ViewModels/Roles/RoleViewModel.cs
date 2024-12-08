using Microsoft.AspNetCore.Identity;

namespace BlogApp.ViewModels
{
	public class RoleViewModel
	{
		public IdentityRole Role { get; set; }
		public RoleViewModel(IdentityRole role)
		{
			Role = role;
		}
	}
}

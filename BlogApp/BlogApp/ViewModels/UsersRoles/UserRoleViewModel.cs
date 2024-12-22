using BlogApp.ViewModels.UsersRoles.Roles;
using BlogApp.ViewModels.UsersRoles.Users;

namespace BlogApp.ViewModels.UsersRoles
{
    public class UserRoleViewModel
	{
		public UserEditViewModel UserEditModel { get; set; }
		public ListRolesViewModel ListRolesModel { get; set; }
		public UserRoleViewModel(UserEditViewModel userEditViewModel,ListRolesViewModel listRolesViewModel) 
		{
			UserEditModel = userEditViewModel;
			ListRolesModel = listRolesViewModel;
		}
	}
}

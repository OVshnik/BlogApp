using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Roles
{
    public class CreateRoleViewModel
	{
		[Remote(action: "CheckRoleName", controller: "Role", ErrorMessage = "роль с таким именем уже существует в базе")]
		[DataType(DataType.Text)]
		[Display(Name = "Роль", Prompt = "Укажите роль")]
		public string Name { get; set; } = String.Empty;
		public string? Description { get; set; } = String.Empty;
	}
}

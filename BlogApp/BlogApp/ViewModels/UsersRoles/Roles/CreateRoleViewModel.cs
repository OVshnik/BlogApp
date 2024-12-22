using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.UsersRoles.Roles
{
    public class CreateRoleViewModel
    {
        [Remote(action: "CheckRoleName", controller: "Role", ErrorMessage = "роль с таким именем уже существует в базе")]
        [DataType(DataType.Text)]
        [Display(Name = "Роль", Prompt = "Укажите роль")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.UsersRoles.Roles
{
    public class EditRoleViewModel
    {
        public string Id { get; set; } = string.Empty;
        [DataType(DataType.Text)]
        [Display(Name = "Роль", Prompt = "Поменять роль")]
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}

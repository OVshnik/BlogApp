using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Roles
{
    public class CreateRoleViewModel
	{
		[DataType(DataType.Text)]
		[Display(Name = "Роль", Prompt = "Укажите роль")]
		public string Name { get; set; } = String.Empty;
	}
}

using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Roles
{
    public class EditRoleViewModel
    {
		public string Id { get; set; } = String.Empty;
		[DataType(DataType.Text)]
		[Display(Name = "Роль", Prompt = "Поменять роль")]
		public string Name { get; set; } = String.Empty;
		public string NormalizedName { get; set; }=String.Empty;
		public string ?Description {  get; set; } = String.Empty;
	}
}

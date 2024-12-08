using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Tags
{
	public class CreateTagViewModel
	{
		[DataType(DataType.Text)]
		[Display(Name = "Тэг", Prompt = "Укажите тэг")]
		public string Name { get; set; } = string.Empty;
		public string Type { get; set; } = string.Empty;
		public Guid ArticleId { get; set; }
	}
}

using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Comments
{
	public class CreateCommentViewModel
	{
		[DataType(DataType.Text)]
		[Display(Name = "Комментарий", Prompt = "Поменять комментарий")]
		public string? Content { get; set; }
		public Guid ArticleId { get; set; }
		public string CommentMakerId { get; set; }= string.Empty;
	}
}

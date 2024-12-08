using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.Comments
{
	public class EditCommentViewModel
	{
		public Guid Id { get; set; }
		[DataType(DataType.Text)]
		[Display(Name = "Комментарий", Prompt = "Поменять комментарий")]
		public string? Content { get; set; }
		public Guid ArticleId { get; set; }
		public string CommentMakerId { get; set; }=String.Empty;
	}
}

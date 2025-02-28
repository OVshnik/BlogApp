using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.ViewModels.Comments
{
	public class EditCommentViewModel
	{
		public Guid Id { get; set; }
		[DataType(DataType.Text)]
		[Required]
		[Display(Name = "Комментарий", Prompt = "Поменять комментарий")]
		public string Content { get; set; } = "";
		public Guid ArticleId { get; set; }
		public string CommentMakerId { get; set; }=String.Empty;
	}
}

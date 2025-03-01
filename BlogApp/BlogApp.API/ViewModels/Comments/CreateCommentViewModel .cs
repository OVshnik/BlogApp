using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.ViewModels.Comments;

public class CreateCommentViewModel
{
	[Required(ErrorMessage = "Поле обязательно для заполнения")]
	[DataType(DataType.Text)]
	[Display(Name = "Комментарий", Prompt = "...")]
	public string Content { get; set; } = "";
	public Guid ArticleId { get; set; }
	public string CommentMakerId { get; set; }= string.Empty;
}
